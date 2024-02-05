using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ChargingTransactionRepository(DataContext context,
                                            IMapper mapper,
                                            IFazaaRepository fazaaRepository,
                                            IPassengerRepository passengerRepository
    ) : IChargingTransactionRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IFazaaRepository _fazaaRepository = fazaaRepository;
        private readonly IPassengerRepository _passengerRepository = passengerRepository;

        public async Task<bool> CreateChargingTransaction(ChargingTransactionCreateDto chargingTransactionCreateDto, int id)
        {
            var chargeMethod = ChargingMethod.MASTERCARD;
            if (chargingTransactionCreateDto.paymentMethod?.ToUpper() == ChargingMethod.MASTERCARD.ToString())
                chargeMethod = ChargingMethod.MASTERCARD;
            else if (chargingTransactionCreateDto.paymentMethod?.ToUpper() == ChargingMethod.VISA.ToString())
                chargeMethod = ChargingMethod.VISA;
            else if (chargingTransactionCreateDto.paymentMethod?.ToUpper() == ChargingMethod.FAZAA.ToString())
                chargeMethod = ChargingMethod.FAZAA;
            else if (chargingTransactionCreateDto.paymentMethod?.ToUpper() == ChargingMethod.BALANCE_TRANSFER.ToString())
                chargeMethod = ChargingMethod.BALANCE_TRANSFER;

            ChargingTransaction chargingTransaction = new()
            {
                ChargingMethod = chargeMethod,
                Amount = chargingTransactionCreateDto.Amount * 100,
                PassengerId = id,
                TimeStamp = DateTime.UtcNow
            };
            double Amount = chargingTransactionCreateDto.Amount * 100;
            var fazaas = await _fazaaRepository.GetFazaasByPassengerId(id);
            if (fazaas != null)
            {
                foreach (var fazaa in fazaas)
                {
                    var passenger = await _passengerRepository.GetPassengerById(fazaa!.Creditor!.Id);
                    if (fazaa!.Amount > 0)
                    {
                        if (fazaa.Amount > Amount)
                        {

                        }
                        else
                        {
                            passenger!.Wallet += fazaa.Amount;
                            Amount -= fazaa.Amount;
                            fazaa.Paid = true;
                        }
                    }
                }
            }
            // (await _context.Passengers.FindAsync(id))!.Wallet += chargingTransactionCreateDto.Amount * 100;
            await _context.ChargingTransactions.AddAsync(chargingTransaction);
            return true;
        }

        public async Task<ChargingTransactionDto?> GetChargingTransactionById(int id)
        {
            return await _context
            .ChargingTransactions
            .Where(ct => ct.Id == id)
            .ProjectTo<ChargingTransactionDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync()!;
        }

        public async Task<IEnumerable<ChargingTransactionDto?>> GetChargingTransactions()
        {
            return await _context
                .ChargingTransactions
                .ProjectTo<ChargingTransactionDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
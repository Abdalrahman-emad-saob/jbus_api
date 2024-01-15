using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bogus.DataSets;

namespace API.Data
{
    public class ChargingTransactionRepository : IChargingTransactionRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ChargingTransactionRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateChargingTransaction(ChargingTransactionCreateDto chargingTransactionCreateDto, int id)
        {
            var chargeMethod = ChargingMethod.MASTERCARD;
            if(chargingTransactionCreateDto.paymentMethod?.ToUpper() == ChargingMethod.MASTERCARD.ToString())
                chargeMethod = ChargingMethod.MASTERCARD;
            else if(chargingTransactionCreateDto.paymentMethod?.ToUpper() == ChargingMethod.VISA.ToString())
                chargeMethod = ChargingMethod.VISA;
            else if(chargingTransactionCreateDto.paymentMethod?.ToUpper() == ChargingMethod.FAZAA.ToString())
                chargeMethod = ChargingMethod.FAZAA;
            else if(chargingTransactionCreateDto.paymentMethod?.ToUpper() == ChargingMethod.BALANCE_TRANSFER.ToString())
                chargeMethod = ChargingMethod.BALANCE_TRANSFER;
                
            ChargingTransaction chargingTransaction = new()
            {
                ChargingMethod = chargeMethod,
                Amount = chargingTransactionCreateDto.Amount,
                PassengerId = id,
                TimeStamp = DateTime.UtcNow
            };
            _context.Passengers.Find(id)!.Wallet += chargingTransactionCreateDto.Amount;
            _context.ChargingTransactions.Add(chargingTransaction);
            return true;
        }

        public ChargingTransactionDto GetChargingTransactionById(int id)
        {
            return _context
            .ChargingTransactions
            .Where(ct => ct.Id == id)
            .ProjectTo<ChargingTransactionDto>(_mapper.ConfigurationProvider)
            .SingleOrDefault()!;
        }

        public IEnumerable<ChargingTransactionDto> GetChargingTransactions()
        {
            return _context
                .ChargingTransactions
                .ProjectTo<ChargingTransactionDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

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

        public bool CreateChargingTransaction(ChargingTransactionCreateDto chargingTransactionDto)
        {
            var chargeMethod = ChargingMethod.MASTERCARD;
            if(chargingTransactionDto.paymentMethod?.ToLower() == ChargingMethod.MASTERCARD.ToString())
                chargeMethod = ChargingMethod.MASTERCARD;
            else if(chargingTransactionDto.paymentMethod?.ToLower() == ChargingMethod.VISA.ToString())
                chargeMethod = ChargingMethod.VISA;
            ChargingTransaction chargingTransaction = new()
            {
                ChargingMethod = chargeMethod,
                Amount = chargingTransactionDto.Amount,
                PassengerId = chargingTransactionDto.PassengerId
            };
            _context.ChargingTransactions.Add(chargingTransaction);
            return SaveChanges();
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
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ScratchCardRepository(DataContext context, IMapper mapper) : IScratchCardRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> CreateCard(ScratchCardCreateDto scratchCardCreateDto, int number)
        {
            for (int i = 0; i < number; i++)
            {
                var scratchCard = _mapper.Map<ScratchCard>(scratchCardCreateDto);
                scratchCard.Amount = scratchCardCreateDto.Type switch
                {
                    "ONE_JOD" => 100,
                    "TWO_JOD" => 200,
                    "FOUR_JOD" => 400,
                    "FIVE_JOD" => 500,
                    "TEN_JOD" => 1000,
                    _ => 0
                };
                scratchCard.CardNumber = await GenerateUniqueRandomNumber();
                scratchCard.CreatedAt = DateTime.UtcNow;
                scratchCard.Status = ScratchCardStatus.ACTIVE;

                await _context.ScratchCards.AddAsync(scratchCard);
            }

            return true;
        }

        public async Task<IEnumerable<ScratchCardDto?>> GetScratchCards()
        {
            return await _context
            .ScratchCards
            .ProjectTo<ScratchCardDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public string[] GetScratchCardsStatuses()
        {
            return Enum.GetNames(typeof(ScratchCardType));
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ScratchCardDto?> ChargeCard(int CN, int passengerId)
        {
            var scratchCard = await _context.ScratchCards.Where(sc => sc.CardNumber == CN).FirstOrDefaultAsync();
            (await _context.Passengers.FindAsync(passengerId))!.Wallet += scratchCard!.Amount;

            scratchCard!.Status = ScratchCardStatus.USED;
            scratchCard.UsedAt = DateTime.UtcNow;

            return _mapper.Map<ScratchCardDto>(scratchCard);
        }

        public async Task<ScratchCardDto?> GetScratchCardById(int id)
        {
            return await _context
            .ScratchCards
            .ProjectTo<ScratchCardDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(card => card.Id == id);
        }
        public async Task<ScratchCardDto?> GetScratchCardByCN(int CardNumber)
        {
            return await _context
            .ScratchCards
            .ProjectTo<ScratchCardDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(card => card.CardNumber == CardNumber);
        }

        public async Task UpdateExpiredCards()
        {
            var now = DateTime.UtcNow;
            var expiredCards = _context.ScratchCards.Where(card => card.ExpiryDate < now);

            foreach (var card in expiredCards)
            {
                card.Status = ScratchCardStatus.EXPIRED;
            }

            await SaveChanges();
        }

        private readonly Random _random = new();

        public async Task<int> GenerateUniqueRandomNumber()
        {
            int number;
            bool numberExists;

            do
            {
                number = _random.Next(100000, 999999);
                numberExists = await _context.ScratchCards.AnyAsync(sc => sc.CardNumber == number);
            }
            while (numberExists);

            return number;
        }
    }
}
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

        public async Task<ScratchCardDto?> CreateCard(ScratchCardCreateDto scratchCardCreateDto)
        {
            var scratchCard = _mapper.Map<ScratchCard>(scratchCardCreateDto);
            scratchCard.CreatedAt = DateTime.UtcNow;
            scratchCard.Status = ScratchCardStatus.ACTIVE;

            await _context.ScratchCards.AddAsync(scratchCard);

            return _mapper.Map<ScratchCardDto>(scratchCard);
        }

        public async Task<IEnumerable<ScratchCardDto?>> GetScratchCards()
        {
            return await _context
            .ScratchCards
            .ProjectTo<ScratchCardDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ScratchCardDto?> ChargeCard(int id, int passengerId)
        {
            var scratchCard = await _context.ScratchCards.FindAsync(id);
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
    }
}
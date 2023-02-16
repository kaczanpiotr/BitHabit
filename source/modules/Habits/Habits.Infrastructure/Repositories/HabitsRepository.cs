using Habits.Domain.Entities;
using Habits.Domain.Repositories;
using Habits.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Habits.Infrastructure.Repositories
{
    public class HabitsRepository : IHabitsRepository
    {
        private readonly HabitsContext _context;

        public HabitsRepository(HabitsContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Habit habit)
        {
            await _context.Habits.AddAsync(habit);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Habit>> BrowseAsync()
        {
            return await _context.Habits
                .Include(h => h.Reminders)
                .Include(h => h.Progress)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Habit>> BrowseAsync(Expression<Func<Habit, bool>> predicate)
        {
            return await _context.Habits
                .Include(h => h.Reminders)
                .Include(h => h.Progress)
                .Where(predicate).ToListAsync();
        }

        public async Task<Habit> GetAsync(HabitId id)
        {
            return await _context.Habits
                .Include(h => h.Reminders)
                .Include(h => h.Progress)
                .SingleOrDefaultAsync(h => h.Id == id);
        }

        public async Task Remove(Habit habit)
        {
            _context.Habits.Remove(habit);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Habit habit)
        {
            _context.Habits.Update(habit);
            await _context.SaveChangesAsync();
        }
    }
}

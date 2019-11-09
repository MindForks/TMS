using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TMS.Entities;
using TMS.Interfaces;

namespace TMS.Data.Repositories
{
    public class TaskRepository : IRepository<Task>
    {
        private readonly DbSet<Task> db;
        private readonly TMSDbContext context;

        public TaskRepository(TMSDbContext context)
        {
            this.context = context;
            db = this.context.Set<Task>();
        }

        public void Create(Task item)
        {
            db.Add(item);
        }

        public void Delete(int id)
        {
            var item = db.Find(id);
            if (item != null)
            {
                db.Remove(item);
            }
        }

        public IEnumerable<Task> Filter(Expression<Func<Task, bool>> predicate)
        {
            return GetAllQuery()
            .Where(predicate)
            .ToList();
        }

        public IEnumerable<Task> GetAll()
        {
            return GetAllQuery()
               .ToList();
        }

        public Task GetItem(int id)
        {
            return GetAllQuery()
            .FirstOrDefault(t => t.Id == id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update(Task item)
        {
                var ModeratorsDbSet = context.Set<TaskModerator_User>();
                var ViewersDbSet = context.Set<TaskViewer_User>();
                var LabelsDbSets = context.Set<Task_Label_User>();

                db.Update(item);

                var newModerators = item.Moderators.ToArray();
                var newViewers = item.Viewers.ToArray();
                var newLabels = item.Labels.ToArray();

                var toRemoveModerators = ModeratorsDbSet
                    .Where(id => id.TaskId == item.Id)
                    .Except(newModerators);
                var toAddModerators = newModerators
                    .Except(ModeratorsDbSet);

                var toRemoveViewers = ViewersDbSet
                    .Where(id => id.TaskId == item.Id)
                    .Except(newViewers);
                var toAddViewers = newViewers
                    .Except(ViewersDbSet);

                var toRemoveLabels = LabelsDbSets
                    .Where(i => i.TaskId == item.Id && i.UserId == item.CurrentUserId);
                var toAddLabels = newLabels
                    .Except(LabelsDbSets);

                ModeratorsDbSet.RemoveRange(toRemoveModerators);
                ModeratorsDbSet.AddRange(toAddModerators);

                ViewersDbSet.RemoveRange(toRemoveViewers);
                ViewersDbSet.AddRange(toAddViewers);

                LabelsDbSets.RemoveRange(toRemoveLabels);
                LabelsDbSets.AddRange(toAddLabels);

        }

        private IQueryable<Task> GetAllQuery()
        {
            return db
                .Include(v => v.Moderators)
                .Include(v => v.Viewers)
                .Include(v => v.Labels);

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}

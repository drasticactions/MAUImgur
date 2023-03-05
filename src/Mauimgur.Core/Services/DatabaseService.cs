// <copyright file="DatabaseService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using Microsoft.EntityFrameworkCore;

namespace Mauimgur.Core.Services
{
    /// <summary>
    /// Database Service.
    /// </summary>
    public class DatabaseService : DbContext
    {
        private string databasePath = "database.db";

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseService"/> class.
        /// </summary>
        /// <param name="databasePath">Path to database.</param>
        public DatabaseService(string databasePath = "")
        {
            if (!string.IsNullOrEmpty(databasePath))
            {
                this.databasePath = databasePath;
            }

            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(databasePath)!);
            this.Database.EnsureCreated();
        }

        /// <summary>
        /// Gets the database Path.
        /// </summary>
        public string Location => this.databasePath;

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        public DbSet<Imgur.API.Models.Image>? Images { get; set; }

        public async Task<int> AddOrUpdateImageAsync(Imgur.API.Models.Image image)
        {
            if (await this.Images!.AnyAsync(n => n.Id == image.Id))
            {
                this.Images!.Update(image);
            }
            else
            {
                await this.Images!.AddAsync(image);
            }

            return await this.SaveChangesAsync();
        }

        /// <summary>
        /// Run when configuring the database.
        /// </summary>
        /// <param name="optionsBuilder"><see cref="DbContextOptionsBuilder"/>.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={this.databasePath}");
            optionsBuilder.EnableSensitiveDataLogging();
        }

        /// <summary>
        /// Run when building the model.
        /// </summary>
        /// <param name="modelBuilder"><see cref="ModelBuilder"/>.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.Entity<Imgur.API.Models.Image>().Ignore(n => n.Tags).HasKey(n => n.Id);
        }
    }
}
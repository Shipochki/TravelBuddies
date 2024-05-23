namespace TravelBuddies.Infrastructure
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
    using TravelBuddies.Domain.Entities;

    public static class SeedData
    {
        public static ModelBuilder Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasData([
                    new ApplicationUser()
                    {
                        Id = "050609c6-76d0-4a15-b9d8-bb70da19d1fe",
                        FirstName = "Stefan",
                        LastName = "Petkov",
                        UserName = "stef@abv.bg",
                        NormalizedUserName = "STEF@ABV.BG",
                        Email = "stef@abv.bg",
                        NormalizedEmail = "STEF@ABV.BG",
						PasswordHash = "AQAAAAIAAYagAAAAEPVnteRPP3bxhfn6aX/VLDyvnhpR1G/XMZL+3lTqsFfCBSkw02SQmqBTA3eYyp8iGw==",
                        SecurityStamp = "KSGYS5WQKJHNDDGNY4GS4EX76VXJQ7OI",
                        ConcurrencyStamp = "2f8f0326-49df-4896-bff4-bd06fae02f72",
                    },
                    new ApplicationUser()
                    {
                        Id = "b33a74a9-0021-4c3a-9481-a6fc6a922655",
                        FirstName = "Ivan",
                        LastName = "Marianov",
                        Country = "Bulgaria",
                        City = "Pernik",
                        UserName = "ivan@mail.com",
                        NormalizedUserName = "IVAN@MAIL.COM",
                        Email = "ivan@mail.com",
                        NormalizedEmail = "IVAN@MAIL.COM",
                        PasswordHash = "AQAAAAIAAYagAAAAEAp54NNJZSWCgUnjr3egFsEaeGZn+LaS5ln6Vy5fdWhxgSRunRtN7ZwkbZTzp0m2xw==",
                        SecurityStamp = "6Y6MZSEJ5FKGOXWADDON74PWO7EYV7MJ",
                        ConcurrencyStamp = "fc339f9a-b858-4016-8b88-ed0a66d016f2",
					},
                    new ApplicationUser()
                    {
                        Id = "fd325402-6f4e-4657-8b05-bc141b17cdef",
                        FirstName = "Admin",
                        LastName = "Administrator",
                        UserName = "admin@gmail.com",
                        NormalizedUserName = "ADMIN@GMAIL.COM",
                        Email = "admin@gmail.com",
                        NormalizedEmail = "ADMIN@GMAIL.COM",
                        PasswordHash = "AQAAAAIAAYagAAAAEMh/VZgfL1JHlQRYRVq9jjyXpX4SwQYPxfO6MgLj0fjy5MsBOxg6Orr5MS9h8NV5ww==",
                        SecurityStamp = "D2VRP2BMPH4QOFVUC3LMIHZVDKCALXNZ",
                        ConcurrencyStamp = "f331cadb-1aa2-40a2-9a4f-caf2d56ea45c",
                    },
                    new ApplicationUser(){
                        Id = "750b562c-3842-460e-a47a-44798f150069",
                        FirstName = "Kristin",
                        LastName = "Petkova",
                        Country = "Bulgaria",
                        City = "Varna",
                        UserName = "krisito@mail.com",
                        NormalizedUserName = "KRISITO@MAIL.COM",
						NormalizedEmail = "KRISITO@MAIL.COM",
                        Email = "krisito@mail.com",
                        PasswordHash = "AQAAAAIAAYagAAAAEC9ZZUGGj1XLYXPq+dbIPaDdWrA4Nhs/tRGrUPWfCxQv4H8GZPBDZZDtswrdXaSj6Q==",
                        SecurityStamp = "W7L75PHKOU2YSBQU5R3FPUCNBNZE6GEB",
                        ConcurrencyStamp = "812c85e3-fcd3-4f53-910f-c366a395750a"
					},
                    new ApplicationUser()
                    {
                        Id = "e31879db-e921-4054-aba7-0f8b6a2092c1",
                        FirstName = "Simeon",
                        LastName = "Gerov",
                        Country = "Bulgaria",
                        City = "Burgas",
                        UserName = "simo@mail.com",
                        NormalizedUserName = "SIMO@MAIL.COM",
                        Email = "simo@mail.com",
                        NormalizedEmail = "SIMO@MAIL.COM",
                        PasswordHash = "AQAAAAIAAYagAAAAEI3uBcNSaKrbzS6hwhbYBA7BT2AMqCW6ebzsPs0QlCfWSIYEMqHCUUCIOY/tPL8qzA==",
                        SecurityStamp = "CCM42K6PXVXEJSDTQEKV27CJUFCPUE65",
                        ConcurrencyStamp = "4a4da6c1-d33c-416a-953a-2274f4e1b640"
					}
                ]);

            modelBuilder.Entity<IdentityRole>()
                .HasData([
                    new IdentityRole()
                    {
                        Id = "0e8ac22b-fe9e-4179-b4df-687e3619bd42",
                        Name = "client",
                        NormalizedName = "CLIENT",
					},
                    new IdentityRole()
                    {
                        Id = "57198ec5-bf97-427d-9a79-982db8c08049",
                        Name = "admin",
                        NormalizedName = "ADMIN",
					},
                    new IdentityRole()
                    {
                        Id = "e3561acf-d05f-494a-95b0-7e12433ec036",
                        Name = "driver",
                        NormalizedName = "DRIVER"
					}
                ]);

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData([
                    new IdentityUserRole<string>()
                    {
                        UserId = "050609c6-76d0-4a15-b9d8-bb70da19d1fe",
                        RoleId = "0e8ac22b-fe9e-4179-b4df-687e3619bd42"
                    },
                    new IdentityUserRole<string>()
                    {
                        UserId = "b33a74a9-0021-4c3a-9481-a6fc6a922655",
                        RoleId = "e3561acf-d05f-494a-95b0-7e12433ec036"
                    },
                    new IdentityUserRole<string>()
                    {
                        UserId = "fd325402-6f4e-4657-8b05-bc141b17cdef",
                        RoleId = "57198ec5-bf97-427d-9a79-982db8c08049"
                    },
                    new IdentityUserRole<string>()
                    {
                        UserId = "750b562c-3842-460e-a47a-44798f150069",
                        RoleId = "0e8ac22b-fe9e-4179-b4df-687e3619bd42",
					},
                    new IdentityUserRole<string>()
                    {
                        UserId = "e31879db-e921-4054-aba7-0f8b6a2092c1",
                        RoleId = "e3561acf-d05f-494a-95b0-7e12433ec036"
					}
                ]);

            return modelBuilder;
        }
    }
}

using Core.Models;
using System;
using System.Collections.Generic;

namespace UnitTests.Services
{
    public class BookServiceBaseTest
    {
        internal List<CartPosition> CorrectCartPositions()
        {
            return new List<CartPosition>
            {
                new CartPosition
                {
                    Book = new Book(
                        Guid.NewGuid(), "Władca Pierścieni",
                        50, 413, "short Description", 11, Guid.NewGuid()),
                    NumberOfBooks = 2,
                    Price = 100
                }
            };
        }

        internal List<CartPosition> FailedCartPositions()
        {
            return new List<CartPosition>
            {
                new CartPosition
                {
                    Book = new Book(
                        Guid.NewGuid(), "Władca Pierścieni",
                        50, 413, "short Description", 4, Guid.NewGuid()),
                    NumberOfBooks = 5,
                    Price = 100
                }
            };
        }
    }
}

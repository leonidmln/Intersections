﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using IntersectionFinder.Infrastructure;

namespace IntersectionFinder.Infrastructure
{
    public partial class IntersectionsContext
    {

        [DbFunction("CheckSegmentIntersection", "dbo")]
        public static bool CheckSegmentIntersection(double? startX1, double? startY1, double? endX1, double? endY1, double? startX2, double? startY2, double? endX2, double? endY2)
        {
            throw new NotSupportedException("This method can only be called from Entity Framework Core queries");
        }

        protected void OnModelCreatingGeneratedFunctions(ModelBuilder modelBuilder)
        {
        }
    }
}
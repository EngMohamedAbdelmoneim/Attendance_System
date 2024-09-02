﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models;

public partial class Department
{

	[Key]
    public int Deptid { get; set; }

	[Required]
	[StringLength(50, MinimumLength = 3)]
	public string DeptName { get; set; }

	[Range(10, 50)]
	public int? Capacity { get; set; }
}
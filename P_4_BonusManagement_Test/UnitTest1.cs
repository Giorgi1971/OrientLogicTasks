using P_4_BonusManagement.Data.Entity;
using FluentAssertions;
using NUnit.Framework;
using P_4_BonusManagement.Services;
using System;


namespace P_4_BonusManagement_Test;

public class Tests
{
    private List<BonusEntity> _bonuses;
    private List<EmployeeEntity> _employees;
    private CalculateStatistic _calc;

    [SetUp]

    public void Setup()
    {
        var employee1 = new EmployeeEntity();
        employee1.EmployeeEntityId = 1;
        employee1.RecommenderId = 0;

        var employee2 = new EmployeeEntity();
        employee2.EmployeeEntityId = 2;
        employee2.RecommenderId = employee1.EmployeeEntityId;

        var employee3 = new EmployeeEntity();
        employee3.EmployeeEntityId = 3;
        employee3.RecommenderId = employee2.EmployeeEntityId;

        var employee4 = new EmployeeEntity();
        employee4.EmployeeEntityId = 4;
        employee4.RecommenderId = employee3.EmployeeEntityId;

        var bonus1 = new BonusEntity();
        bonus1.EmployeeEntityId = employee3.EmployeeEntityId;
        bonus1.EmployeeEntity = employee3;
        bonus1.BonusAmount = 500;

        var bonus2 = new BonusEntity();
        bonus2.EmployeeEntityId = employee2.EmployeeEntityId;
        bonus2.EmployeeEntity = employee2;
        bonus2.BonusAmount = 250;

        var bonus3 = new BonusEntity();
        bonus3.EmployeeEntityId = employee3.EmployeeEntityId;
        bonus3.EmployeeEntity = employee3;
        bonus3.BonusAmount = 125;

        _bonuses = new List<BonusEntity> { bonus1, bonus2, bonus3 };
        _employees = new List<EmployeeEntity> { employee1, employee2, employee3, employee4 };
        _calc = new CalculateStatistic();
    }

    // [Test] - როცა ეწერა ქვევით აწითლებდა.
    [Test]
    public void Test1()
    {
        var topBonusedEmployees = _calc.CalculateTopBonused(_employees, _bonuses).Result;
        Assert.Multiple(() =>
        {
            Assert.That(topBonusedEmployees[0].TotalAmount, Is.EqualTo(625));
            Assert.That(topBonusedEmployees[1].TotalAmount, Is.EqualTo(250));
            Assert.That(topBonusedEmployees[1].EmoloyeeId, Is.EqualTo(2));
            Assert.That(topBonusedEmployees[0].EmoloyeeId, Is.EqualTo(3));
        });
    }

    [Test]
    public void ShouldReturnCorrectTopReferrers()
    {
        var recommenders = _calc.CalculateTopRecomendator(_employees, _bonuses).Result;
        recommenders.Count.Should().Be(2);
        recommenders[0].RecId.Should().Be(_employees[1].EmployeeEntityId);
        recommenders[0].ReferalAmount.Should().Be(625);
        recommenders[1].RecId.Should().Be(_employees[0].EmployeeEntityId);
        recommenders[1].ReferalAmount.Should().Be(250);
    }

}


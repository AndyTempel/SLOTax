﻿// <copyright file="NU_Barcodes.cs" company="MNet">
//     Copyright (c) Matjaz Prtenjak All rights reserved.
// </copyright>
// <author>Matjaz Prtenjak</author>
//-----------------------------------------------------------------------

using System;
using System.Text;
using MNet.SLOTaxService.Utils;
using NUnit.Framework;

namespace MNet.SLOTaxService.UnitTests
{
  internal class NU_Barcodes
  {
    [Test]
    public void ConvertHex2DecTest()
    {
      string hexNumber = "a7e5f55e1dbb48b799268e1a6d8618a3";
      string dec = BarCodesHelpers.HexToDecimal(hexNumber);

      StringAssert.AreEqualIgnoringCase(dec, "223175087923687075112234402528973166755");
    }

    [Test]
    public void Modulo10Test1()
    {
      string value = "543778220634452";
      int modulo10 = BarCodesHelpers.CalculateModulo10(value);
      Assert.AreEqual(modulo10, 0);
    }

    [Test]
    public void Modulo10Test2()
    {
      string value = "22317508792368707511223440252897316675512345678150815101332";
      int modulo10 = BarCodesHelpers.CalculateModulo10(value);
      Assert.AreEqual(modulo10, 7);
    }

    [Test]
    public void Modulo10RandomTest()
    {
      Random rnd = new Random();

      for (int i = 0; i < 10; i++)
      {
        int len = rnd.Next(10, 23);

        StringBuilder sb = new StringBuilder(len + 2);
        for (int j = 0; j < len; j++)
          sb.Append((char)('0' + rnd.Next(0, 9)));

        this.doCheck10(sb.ToString());
      }
    }

    [Test]
    public void generateBarCodeTest1()
    {
      string barCode = BarCodesHelpers.GenerateCode("a7e5f55e1dbb48b799268e1a6d8618a3", "12345678", Convert.ToDateTime("2015-08-15 10:13:32"));
      StringAssert.AreEqualIgnoringCase(barCode, "223175087923687075112234402528973166755123456781508151013327");
    }

    [Test]
    public void generateBarCodeTest2()
    {
      string barCode = BarCodesHelpers.GenerateCode("1234567890abcdef", "24578436", Convert.ToDateTime("2017-01-11 18:00:32"));
      StringAssert.AreEqualIgnoringCase(barCode, "000000000000000000001311768467294899695245784361701111800322");
    }

    private void doCheck10(string randomValue)
    {
      string value = BarCodesHelpers.AppendModulo10(randomValue);
      bool check = BarCodesHelpers.CheckModulo10(value);
      Assert.True(check);
    }
  }
}

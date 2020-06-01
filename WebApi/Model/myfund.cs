﻿
using Dapper.FluentMap.Mapping;
using MSS.Platform.Workflow.WebApi.Data;
using System;
using System.Collections.Generic;


// Coded by admin 2020/5/29 14:19:11
namespace MSS.Platform.Workflow.WebApi.Model
{
    public class MyfundParm : BaseQueryParm
    {

    }
    public class MyfundPageView
    {
        public List<Myfund> rows { get; set; }
        public int total { get; set; }
    }

    public class Myfund : BaseEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public decimal Costavg { get; set; }
        public decimal Networth { get; set; }
        public decimal Totalworth { get; set; }
        public System.DateTime Worthdate { get; set; }
        public decimal Daygrowth { get; set; }
        public System.DateTime Updatetime { get; set; }
    }
    public class FundRetComm
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<FundRet> data { get; set; }
    }

    public class FundRet
    {
        public string code { get; set; }
        public string name { get; set; }
        public decimal netWorth { get; set; }
        public decimal totalWorth { get; set; }
        public decimal dayGrowth { get; set; }
        public DateTime worthDate { get; set; }
    }

    public class MyfundMap : EntityMap<Myfund>
    {
        public MyfundMap()
        {
            Map(o => o.Id).ToColumn("id");
            Map(o => o.Code).ToColumn("code");
            Map(o => o.Name).ToColumn("name");
            Map(o => o.Balance).ToColumn("balance");
            Map(o => o.Costavg).ToColumn("costavg");
            Map(o => o.Networth).ToColumn("networth");
            Map(o => o.Totalworth).ToColumn("totalworth");
            Map(o => o.Worthdate).ToColumn("worthdate");
            Map(o => o.Daygrowth).ToColumn("daygrowth");
            Map(o => o.Updatetime).ToColumn("updatetime");
        }
    }

}
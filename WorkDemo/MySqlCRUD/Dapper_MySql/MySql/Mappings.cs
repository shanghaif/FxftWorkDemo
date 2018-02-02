//using BulkCommon;
using DapperExtensions.Mapper;
//using FlowDataModel;

namespace Dapper_MySql
{
    public static class Mappings
    {
        public static void Initialize()
        {
            DapperExtensions.DapperExtensions.DefaultMapper = typeof(PluralizedAutoClassMapper<>);

            DapperExtensions.DapperExtensions.SetMappingAssemblies(new[]
            {
                typeof(Mappings).Assembly
            });
        }

        //public class FlowCellMapper : ClassMapper<FlowCell>
        //{
        //    public FlowCellMapper()
        //    {
        //        Table("jxc_flow_cell");
        //        //Map(fcel => fcel.id).Column("id"); 
        //        //Map(fcel => fcel.parent_id).Column("parent_id"); 
        //        //Map(fcel => fcel.create_time).Column("create_time"); 
        //        //Map(fcel => fcel.type_id).Column("type_id"); 
        //        Map(fcel => fcel.comId).Column("bloc_code");
        //        //Map(fcel => fcel.bloc_name).Column("bloc_name"); 
        //        //Map(fcel => fcel.cell_number).Column("cell_number"); 
        //        //Map(fcel => fcel.name).Column("name"); 
        //        //Map(fcel => fcel.flows).Column("flows"); 
        //        //Map(fcel => fcel.status).Column("status"); 
        //        //Map(fcel => fcel.del).Column("del"); 
        //        AutoMap();
        //    }
        //}

        public class SyncLogMapper : ClassMapper<SyncLog>
        {
            public SyncLogMapper()
            {
                Table("datasyn_log");
                AutoMap();
            }
        }
    }

    ///// <summary>
    ///// 客户账户明细
    ///// </summary>
    //public class CustomerAccountDetailItemMapper : ClassMapper<CustomerAccountDetailItem>
    //{
    //    public CustomerAccountDetailItemMapper()
    //    {
    //        Table("view_account_log");
    //        Map(item => item.cNo).Column("custom_no");
    //        Map(item => item.oNo).Column("sn_number");
    //        AutoMap();
    //    }
    //}

    ///// <summary>
    ///// 客户续费记录
    ///// </summary>
    //public class CustomerRenewItemMapper : ClassMapper<CustomerRenewItem>
    //{
    //    public CustomerRenewItemMapper()
    //    {
    //        Table("view_custom_renew");
    //        Map(item => item.cNo).Column("custom_no");
    //        Map(item => item.renewNo).Column("sn");
    //        Map(item => item.simCount).Column("sim_count");
    //        Map(item => item.amount).Column("amount_money");
    //        Map(item => item.payType).Column("paytype");
    //        Map(item => item.payTime).Column("pay_time");
    //        Map(item => item.doUser).Column("do_user");
    //        Map(item => item.payStatus).Column("status");
    //        Map(item => item.createTime).Column("create_time");
    //        Map(item => item.renewType).Column("goods_type");

    //        AutoMap();
    //    }
    //}

    ///// <summary>
    ///// 客户续费明细
    ///// </summary>
    //public class CustomerRenewDetailItemMapper : ClassMapper<CustomerRenewDetailItem>
    //{
    //    public CustomerRenewDetailItemMapper()
    //    {
    //        Table("view_custom_renew_detail");
    //        Map(item => item.cNo).Column("custom_no");
    //        Map(item => item.renewNo).Column("sn");
    //        Map(item => item.payType).Column("paytype");
    //        Map(item => item.payTime).Column("pay_time");
    //        Map(item => item.doUser).Column("do_user");
    //        Map(item => item.payStatus).Column("status");
    //        Map(item => item.createTime).Column("ctime");
    //        Map(item => item.renewType).Column("goods_type");
    //        Map(item => item.customName).Column("custom_name");
    //        Map(item => item.goodsName).Column("goods_name");
    //        Map(item => item.unitPrice).Column("unit_price");
    //        Map(item => item.amountPrice).Column("amount_price");
    //        Map(item => item.quantity).Column("quantity");

    //        AutoMap();
    //    }
    //}

    /// <summary>
    /// 卡类型
    /// </summary>
    //public class SyncLogMapper : ClassMapper<SyncLog>
    //    {
    //        public SyncLogMapper()
    //        {
    //            Table("datasyn_log");
    //            AutoMap();
    //        }
    //    }

        ///// <summary>
        ///// 集团名称
        ///// </summary>
        //public class ComInfoMapper : ClassMapper<ComInfo>
        //{
        //    public ComInfoMapper()
        //    {
        //        Table("jxc_bloc_manage");
        //        AutoMap();
        //    }
        //}
    }

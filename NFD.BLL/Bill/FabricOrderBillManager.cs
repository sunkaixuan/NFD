﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFD.Entities.Data;
using COM.Extension;
namespace NFD.BLL.Bill
{
    /// <summary>
    /// 面料订购单
    /// </summary>
    public class FabricOrderBillManager
    {
        /// <summary>
        /// 获取裁剪
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static IQueryable<FabricOrderBill> GetFabricOrderBillList(NFDEntities db, int orderId)
        {
            if (orderId == 0)
            {
                return db.FabricOrderBill.Where(c => c.is_del == null || c.is_del == false);
            }
            else
                return db.FabricOrderBill.Where(c => c.order_id == orderId).Where(c => c.is_del == null || c.is_del == false);
        }

        /// <summary>
        /// 保存裁剪单
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public static bool SaveFabricOrderBillAll(FabricOrderBill bill)
        {
            using (NFDEntities db = new NFDEntities())
            {
                if (bill.fob_id.IsZero())
                {//添加
                    var orderBill = db.OrderBill.Single(c => c.o_id == bill.order_id);
                    bill.no = orderBill.no;
                    bill.creator_id = UserManager.GetCurrentUserInfo.user_id;
                    bill.creator_name = UserManager.GetCurrentUserInfo.userName;
                    db.FabricOrderBill.AddObject(bill);
                    var isSaved = db.SaveChanges() > 0;
                    return isSaved;
                }
                else
                {
                    //编辑
                    db.Update<FabricOrderBill>(bill.fob_id, new

                    {
                        datum = bill.datum,
                        get_date = bill.get_date,
                        no = bill.no,
                        num = bill.num,
                        price = bill.price,
                        element = bill.element,
                        trader_id = bill.trader_id,
                        clothing_number = bill.clothing_number,
                        address = bill.address,
                        area = bill.area,
                        color_foreign = bill.color_foreign,
                        color_name = bill.color_name,
                        specifications = bill.specifications,
                        supplier_id = bill.supplier_id,
                        sdf = bill.sdf,
                        remark = bill.remark



                    });

                    return true;
                }

            }
        }

        /// <summary>
        /// 删除裁剪单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DelBill(params int[] id)
        {
            using (NFDEntities db = new NFDEntities())
            {
                return db.FalseDelete<FabricOrderBill>(id);
            }

        }



    }
}
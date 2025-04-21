using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Convertor;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.SiteSetting;
using Shop.Application.DTOs.Ticket;
using Shop.Application.DTOs.User;
using Shop.Domain.Models;
using Shop.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Shop.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IOrderStatusRepository _orderStatusRepo;

        public OrderRepository(DatabaseContext context, IMapper mapper, IOrderStatusRepository orderStatusRepo) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _orderStatusRepo = orderStatusRepo;

        }



        public string GenerateWordOfOrder(OrderDetailShowDto order)
        {
            var source = "wwwroot/Shared/Factors/Surathesab.docx";
            var filename = "";
            var result = "";



            using (var document = DocX.Load(source))
            {
                var name = $"{order.Costumer.FirstName} {order.Costumer.LastName}";
                if (order.Costumer.Phone == null)
                {
                    order.Costumer.Phone = "-";
                }
                if (order.Costumer.NationalCode == null)
                {
                    order.Costumer.NationalCode = "-";
                }
                if (order.Costumer.Address == null)
                {
                    order.Costumer.Address = "-";
                }
                if (order.Costumer.PostCode == null)
                {
                    order.Costumer.PostCode = "-";
                }
                if (order.Costumer.EconomicCode == null)
                {
                    order.Costumer.EconomicCode = "-";
                }

                //var date = WordDate(order.CreateDate.ToShamsi());
                document.ReplaceText("{سریال}", order.Order.OrderCode, false, RegexOptions.IgnoreCase);
                document.ReplaceText("{تاریخ}", order.Order.CreateDate.ToShamsi(), false, RegexOptions.IgnoreCase);
                document.ReplaceText("{شرکت}", name, false, RegexOptions.IgnoreCase);

                document.ReplaceText("{تلفن}", order.Costumer.Phone, false, RegexOptions.IgnoreCase);
                document.ReplaceText("{پرسنلی}", order.Costumer.EconomicCode, false, RegexOptions.IgnoreCase);

                document.ReplaceText("{نشانی}", order.Costumer.Address, false, RegexOptions.IgnoreCase);
                document.ReplaceText("{ملی}", order.Costumer.NationalCode, false, RegexOptions.IgnoreCase);
                document.ReplaceText("{پستی}", order.Costumer.PostCode, false, RegexOptions.IgnoreCase);


               

                if (order.OrderDetailDto.Count() > 0)
                {
                    // جستجوی جدول مورد نظر برای پر کردن
                    var table = document.Tables.FirstOrDefault(t => t.Rows[0].Cells[0].Paragraphs.First().Text == "*");
                    if (table != null)
                    {
                        // پر کردن سلول‌های جدول با داده‌های مورد نظر
                        for (int i = 0; i < order.OrderDetailDto.Count; i++)
                        {

                            var cell5 = order.OrderDetailDto[i].Price * 10; ;
                            var cell6 = order.OrderDetailDto[i].DetailSum * 10;
                            var cell7 = order.OrderDetailDto[i].Discount * 10;
                            var cell8 = order.OrderDetailDto[i].Tax * 10;
                            var cell9 = order.OrderDetailDto[i].Total * 10;



                            var row = i + 1;
                            var newRow = table.InsertRow(table.Rows.First());
                            newRow.Cells[0].Paragraphs.First().ReplaceText("*", row.ToString(), false, RegexOptions.IgnoreCase);
                            newRow.Cells[0].Paragraphs.First().Alignment = Alignment.right;
                            newRow.Cells[1].Paragraphs.First().ReplaceText("کد کالا", order.OrderDetailDto[i].ProductCode, false, RegexOptions.IgnoreCase);
                            newRow.Cells[2].Paragraphs.First().ReplaceText("شرح کالا یا خدمات", order.OrderDetailDto[i].ProductName, false, RegexOptions.IgnoreCase);
                            newRow.Cells[2].Paragraphs.First().Alignment = Alignment.left;
                            newRow.Cells[3].Paragraphs.First().ReplaceText("تعداد", order.OrderDetailDto[i].Count.ToString(), false, RegexOptions.IgnoreCase);
                            newRow.Cells[4].Paragraphs.First().ReplaceText("واحد", "عدد", false, RegexOptions.IgnoreCase);
                            newRow.Cells[5].Paragraphs.First().ReplaceText("مبلغ واحد ریال", cell5.ToString("#,0"), false, RegexOptions.IgnoreCase);
                            newRow.Cells[6].Paragraphs.First().ReplaceText("مبلغ کل ریال", cell6.ToString("#,0"), false, RegexOptions.IgnoreCase);

                            newRow.Cells[7].Paragraphs.First().ReplaceText("مبلغ تخفیف ریال", cell7.ToString("#,0"), false, RegexOptions.IgnoreCase);
                            //newRow.Cells[8].Paragraphs.First().ReplaceText("مبلغ کل پس ازتخفیف ریال", order.OrderDetailDto[i].Discount.ToString("#,0"), false, RegexOptions.IgnoreCase);
                            newRow.Cells[8].Paragraphs.First().ReplaceText("مالیات و عوارض ریال", cell8.ToString("#,0"), false, RegexOptions.IgnoreCase);
                            newRow.Cells[9].Paragraphs.First().ReplaceText("جمع مبلغ کل مالیات و عوارض ریال", cell9.ToString("#,0"), false, RegexOptions.IgnoreCase);

                        }



                        var totalCell6 = order.Order.OrderSum * 10;
                        var totalCell7 = order.Order.Discount * 10;
                        var totalCell8 = order.Order.Tax * 10;
                        var totalCell9 = order.Order.Total * 10;


                        var lastRow = table.InsertRow(table.Rows.First());
                        lastRow.Cells[0].Paragraphs.First().ReplaceText("*", "", false, RegexOptions.IgnoreCase);
                        lastRow.Cells[1].Paragraphs.First().ReplaceText("کد کالا", "", false, RegexOptions.IgnoreCase);



                        lastRow.Cells[2].Paragraphs.First().ReplaceText("شرح کالا یا خدمات", "جمع کل", false, RegexOptions.IgnoreCase);
                        lastRow.Cells[2].Paragraphs.First().Alignment = Alignment.left;

                        lastRow.Cells[3].Paragraphs.First().ReplaceText("تعداد", "", false, RegexOptions.IgnoreCase);
                        lastRow.Cells[4].Paragraphs.First().ReplaceText("واحد", "", false, RegexOptions.IgnoreCase);

                        lastRow.Cells[5].Paragraphs.First().ReplaceText("مبلغ واحد ریال", "", false, RegexOptions.IgnoreCase);
                        lastRow.Cells[6].Paragraphs.First().ReplaceText("مبلغ کل ریال", totalCell6.ToString("#,0"), false, RegexOptions.IgnoreCase);

                        lastRow.Cells[7].Paragraphs.First().ReplaceText("مبلغ تخفیف ریال", totalCell7.ToString("#,0"), false, RegexOptions.IgnoreCase);
                        //lastRow.Cells[8].Paragraphs.First().ReplaceText("مبلغ کل پس ازتخفیف ریال", order.Order.Discount.ToString("#,0"), false, RegexOptions.IgnoreCase);
                        lastRow.Cells[8].Paragraphs.First().ReplaceText("مالیات و عوارض ریال", totalCell8.ToString("#,0"), false, RegexOptions.IgnoreCase);
                        lastRow.Cells[9].Paragraphs.First().ReplaceText("جمع مبلغ کل مالیات و عوارض ریال", totalCell9.ToString("#,0"), false, RegexOptions.IgnoreCase);
                        lastRow.MergeCells(1, 4);



                        if (order.OrderPay.Count() > 0)
                        {
                            var payRow = table.InsertRow(table.Rows.First());
                            payRow.Cells[0].Paragraphs.First().ReplaceText("*", "", false, RegexOptions.IgnoreCase);
                            payRow.Cells[1].Paragraphs.First().ReplaceText("کد کالا", "", false, RegexOptions.IgnoreCase);
                            payRow.Cells[2].Paragraphs.First().ReplaceText("شرح کالا یا خدمات", "نحوه پرداخت", false, RegexOptions.IgnoreCase);
                            payRow.Cells[2].Paragraphs.First().Alignment = Alignment.left;
                            payRow.Cells[3].Paragraphs.First().ReplaceText("تعداد", "", false, RegexOptions.IgnoreCase);
                            payRow.Cells[4].Paragraphs.First().ReplaceText("واحد", "مبلغ پرداختی", false, RegexOptions.IgnoreCase);
                            payRow.Cells[5].Paragraphs.First().ReplaceText("مبلغ واحد ریال", "", false, RegexOptions.IgnoreCase);
                            payRow.Cells[6].Paragraphs.First().ReplaceText("مبلغ کل ریال", "", false, RegexOptions.IgnoreCase);
                            payRow.Cells[7].Paragraphs.First().ReplaceText("مبلغ تخفیف ریال", "توضیحات", false, RegexOptions.IgnoreCase);
                            payRow.Cells[8].Paragraphs.First().ReplaceText("مالیات و عوارض ریال", "", false, RegexOptions.IgnoreCase);
                            payRow.Cells[9].Paragraphs.First().ReplaceText("جمع مبلغ کل مالیات و عوارض ریال", "", false, RegexOptions.IgnoreCase);
                            payRow.MergeCells(1, 3);
                            payRow.MergeCells(2, 4);
                            payRow.MergeCells(3, 5);

                            for (int i = 0; i < order.OrderPay.Count; i++)
                            {
                                var patDesc = $"{order.OrderPay[i].DepositCode}/{order.OrderPay[i].PayTracking}";
                                var paysCell4 = order.OrderPay[i].Price * 10;
                                var paysRow = table.InsertRow(table.Rows.First());
                                paysRow.Cells[0].Paragraphs.First().ReplaceText("*", "", false, RegexOptions.IgnoreCase);
                                paysRow.Cells[1].Paragraphs.First().ReplaceText("کد کالا", "", false, RegexOptions.IgnoreCase);
                                paysRow.Cells[2].Paragraphs.First().ReplaceText("شرح کالا یا خدمات", order.OrderPay[i].Description, false, RegexOptions.IgnoreCase);
                                paysRow.Cells[2].Paragraphs.First().Alignment = Alignment.left;
                                paysRow.Cells[3].Paragraphs.First().ReplaceText("تعداد", "", false, RegexOptions.IgnoreCase);
                                paysRow.Cells[4].Paragraphs.First().ReplaceText("واحد", paysCell4.ToString("#,0"), false, RegexOptions.IgnoreCase);
                                paysRow.Cells[5].Paragraphs.First().ReplaceText("مبلغ واحد ریال", "", false, RegexOptions.IgnoreCase);
                                paysRow.Cells[6].Paragraphs.First().ReplaceText("مبلغ کل ریال", "", false, RegexOptions.IgnoreCase);
                                paysRow.Cells[7].Paragraphs.First().ReplaceText("مبلغ تخفیف ریال", patDesc, false, RegexOptions.IgnoreCase);
                                paysRow.Cells[8].Paragraphs.First().ReplaceText("مالیات و عوارض ریال", "", false, RegexOptions.IgnoreCase);
                                paysRow.Cells[9].Paragraphs.First().ReplaceText("جمع مبلغ کل مالیات و عوارض ریال", "", false, RegexOptions.IgnoreCase);
                                paysRow.MergeCells(1, 3);
                                paysRow.MergeCells(2, 4);
                                paysRow.MergeCells(3, 5);
                            }


                        }

                        if (order.OrderCoupon.CouponCode != null)
                        {
                            var coupon = $"کد تحفیف استفاده شده : {order.OrderCoupon.CouponCode}";
                            var couponRow = table.InsertRow(table.Rows.First());
                            couponRow.Cells[0].Paragraphs.First().ReplaceText("*", "", false, RegexOptions.IgnoreCase);
                            couponRow.Cells[1].Paragraphs.First().ReplaceText("کد کالا", "", false, RegexOptions.IgnoreCase);
                            couponRow.Cells[2].Paragraphs.First().ReplaceText("شرح کالا یا خدمات", coupon, false, RegexOptions.IgnoreCase);
                            couponRow.Cells[2].Paragraphs.First().Alignment = Alignment.left;
                            couponRow.Cells[3].Paragraphs.First().ReplaceText("تعداد", "", false, RegexOptions.IgnoreCase);
                            couponRow.Cells[4].Paragraphs.First().ReplaceText("واحد", "", false, RegexOptions.IgnoreCase);
                            couponRow.Cells[5].Paragraphs.First().ReplaceText("مبلغ واحد ریال", "", false, RegexOptions.IgnoreCase);
                            couponRow.Cells[6].Paragraphs.First().ReplaceText("مبلغ کل ریال", "", false, RegexOptions.IgnoreCase);
                            couponRow.Cells[7].Paragraphs.First().ReplaceText("مبلغ تخفیف ریال", "", false, RegexOptions.IgnoreCase);
                            couponRow.Cells[8].Paragraphs.First().ReplaceText("مالیات و عوارض ریال", "", false, RegexOptions.IgnoreCase);
                            couponRow.Cells[9].Paragraphs.First().ReplaceText("جمع مبلغ کل مالیات و عوارض ریال", "", false, RegexOptions.IgnoreCase);
                            couponRow.MergeCells(1, 9);
                            
                        }
                    }


                    Random random = new Random();
                    var title = random.Next(10000, 99999);
                    filename = $"wwwroot/Shared/Factors/{order.Order.OrderCode}({title}).docx";
                    result = $"Shared/Factors/{order.Order.OrderCode}({title}).docx";

                    foreach (var data in document.Paragraphs)
                    {
                        data.Direction = Direction.RightToLeft;
                    }
                }

                // ذخیره فایل Word

                document.AddProtection(EditRestrictions.readOnly);

                document.SaveAs(filename);



            }



            return result;
        }

        public double GetEachGhest(int Total, int Nerkh, int Time)
        {
            double ghest = ((Nerkh / 1200) + Math.Pow((1 + Nerkh / 1200), Time) * Total) / (Math.Pow((1 + Nerkh / 1200), Time) - 1);
            return ghest;
        }
        public double GetTotalGhest(int Total, int GhestCount, int Ghest)
        {
            var Karmozd = Total * 0.02;
            double Result = (GhestCount * Ghest) + Karmozd;
            return Result;
        }

        public async Task OrderSum(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(z => z.OrderId == orderId);
            var orderDetails = await _context.OrderDetails.Where(z => z.OrderId == orderId).ToListAsync();
            int orderSum = 0;
            foreach (var orderDetail in orderDetails)
            {
                orderSum += (int)orderDetail.Price * orderDetail.Count;
            }
            order.OrderSum = orderSum;
            UpdateAsync(order);
        }

        public async Task OrderDiscount(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(z => z.OrderId == orderId);
            var orderDetails = await _context.OrderDetails.Where(z => z.OrderId == orderId).ToListAsync();
            int orderDiscount = 0;
            foreach (var orderDetail in orderDetails)
            {
                orderDiscount += (int)orderDetail.Discount;
            }
            order.Discount = orderDiscount;
            UpdateAsync(order);
        }

        public async Task OrderTax(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(z => z.OrderId == orderId);
            var orderDetails = await _context.OrderDetails.Where(z => z.OrderId == orderId).ToListAsync();
            int orderTax = 0;
            foreach (var orderDetail in orderDetails)
            {
                orderTax += (int)orderDetail.Tax;
            }
            order.Tax = orderTax;
            UpdateAsync(order);
        }

        public async Task OrderTotal(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(z => z.OrderId == orderId);
            var orderDetails = await _context.OrderDetails.Where(z => z.OrderId == orderId).ToListAsync();
            int orderTotal = 0;
            foreach (var orderDetail in orderDetails)
            {
                orderTotal += (int)orderDetail.Total * orderDetail.Count;
            }
            order.Total = orderTotal;
            UpdateAsync(order);
        }

        public async Task<Order> GetNotFinallyOrderOfUser(int userId)
        {
            if (await _context.Orders.AnyAsync(u => u.UserId == userId && !u.IsFinaly && !u.IsDelete))
            {
                return await _context.Orders.FirstOrDefaultAsync(z => z.UserId == userId && !z.IsFinaly);
            }
            else
            {
                return new Order();
            }
        }

        public async Task<List<OrderDto>> GetFinallyOrdersOfUser(int userId)
        {

            List<OrderDto> orderDtos = new List<OrderDto>();
            List<Order> orderList = await _context.Orders.Where(z => z.UserId == userId && z.IsFinaly).OrderByDescending(u => u.CreateDate).ToListAsync();
            foreach (var order in orderList)
            {
                var orderDto = _mapper.Map<OrderDto>(order);
                var User = await _context.UserBillings.Where(a => a.UserId == order.UserId).FirstOrDefaultAsync();

                orderDto.Costumer = $"{User.FirstName} {User.LastName}";

                var orderStatus = await _orderStatusRepo.GetActiveOrderStatuseByOrderId(order.OrderId);
                if (orderStatus != null)
                {
                    orderDto.Status = orderStatus.Status.StatusName;
                    orderDto.StatusIcon = orderStatus.Status.Icon;
                }
                else
                {
                    orderDto.Status = "وضعیتی ثبت نشده است";
                    orderDto.StatusIcon = "-";
                }

                orderDtos.Add(orderDto);
            }

            return orderDtos;
        }

        public async Task<PagingDto> GetFinallyOrdersOfUserByPaging(int Pageid = 1, int Take = 30, string Filter = "", int userId = 0)
        {
            var res = await GetFinallyOrdersOfUser(userId);

            IQueryable<OrderDto> result = res.AsQueryable();


            PagingDto response = new PagingDto();

            if (!string.IsNullOrEmpty(Filter))
            {
                result = result.Where(p =>
                    (p.OrderCode.Contains(Filter)) ||
                    (p.IntroCode.Contains(Filter)));
            }

            int skip = (Pageid - 1) * Take;

            response.CurrentPage = Pageid;
            int count = result.Count();
            response.PageCount = count / Take;


            response.List = result.Skip(skip).Take(Take).ToArray();

            return response;
        }

        public async Task<bool> OrderExistAnyDetails(int orderId)
        {
            if (await _context.OrderDetails.AnyAsync(u => u.OrderId == orderId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Order?> GetOpenOrderOfUser(int userId)
        {
            if (await _context.Orders.AnyAsync(u => u.UserId == userId && !u.IsFinaly))
            {
                return await _context.Orders.FirstOrDefaultAsync(u => u.UserId == userId && !u.IsFinaly);
            }
            else
            {
                return null;
            }
        }
    }
}

﻿using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Interfaces
{
    public interface IOrderRepository
    {
        LoadOrderListResponse LoadOrders(string orderDate);
        void SaveOrders(List<Order> orders, string orderDate);
        bool OrderFileExists(string orderDate);


    }
}

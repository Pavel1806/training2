using ADO_NET_DAL.Model;
using ADO_NET_ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADO_NET_DAL.Interfaces
{
    /// <summary>
    /// интерфейс для OrderRepository
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Получение всех Order
        /// </summary>
        /// <returns>Все Order</returns>
        IEnumerable<Order> GetAll();

        /// <summary>
        /// Получение Order по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор Order</param>
        /// <returns></returns>
        Order GetById(int id);

        /// <summary>
        /// Метод создания Order
        /// </summary>
        /// <param name="order">Order который приходит из интерфейса</param>
        int Create(ViewOrder order);

        /// <summary>
        /// Изменение Order
        /// </summary>
        /// <param name="order">Order который приходит из интерфейса</param>
        void Update(ViewOrder order);

        /// <summary>
        /// Метод удаления Order
        /// </summary>
        /// <param name="id">Идентификатор Order</param>
        /// <returns></returns>
        int Delete(int id);

        /// <summary>
        /// Установка даты доставки заказа
        /// </summary>
        /// <param name="id">номер заказа</param>
        int SetTheOrderDay(int id);

        /// <summary>
        /// Установка даты доставки заказа
        /// </summary>
        /// <param name="id">номер заказа</param>
        int InstallOrderCompleted(int id);

        /// <summary>
        /// Вызов хранимой процедуры из базы данных
        /// </summary>
        /// <param name="customer">id покупателя</param>
        Dictionary<string, int> CallingStoredProcedure(string customer);

    }
}

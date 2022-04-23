using ADO_NET_DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADO_NET_DAL.Interfaces
{
    /// <summary>
    /// интерфейс для ProductRepository
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Получение продукта по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Один продукт</returns>
        Product GetById(int id);

        /// <summary>
        /// Уменьшение количества продуктов на складе
        /// </summary>
        /// <param name="productid">идентификатор продукта</param>
        /// <param name="quantity">количество на которое надо умешить количество продуктов</param>
        void DecreaseUnitsInStock(int productid, int quantity);

        /// <summary>
        /// Увеличение количества продуктов на складе
        /// </summary>
        /// <param name="productid">идентификатор продукта</param>
        /// <param name="quantity">количество на которое надо увеличить количество продуктов</param>
        void IncreaseUnitsInStock(int productid, int quantity);
    }
}

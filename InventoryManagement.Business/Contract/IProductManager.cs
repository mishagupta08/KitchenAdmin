using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Business.Contract
{
    public interface IProductManager
    {
        ResponseDetail AddCategoryDetails(CategoryDetails model);
        ResponseDetail IsMasterExists(CheckDuplicateModel model);
        ResponseDetail AddSubCategoryDetails(SubCategoryDetails model);
        List<CategoryDetails> GetCategoryList(string ActiveFlag);
        List<SubCategoryDetails> GetSubcategoryDetails(int CategoryId, string ActiveStatus);
        ResponseDetail SaveProductMaster(ProductDetails model);
        int MaxBarCode();
        int MaxProductCode();
        List<ProductDetails> GetProductList(decimal LoginStateCode);
        ResponseDetail DeleteProductMaster(ProductDetails model);
        ResponseDetail EditProductMaster(ProductDetails model);
        ProductDetails GetProductDetail(decimal ProductId, decimal LoginStateCode);
    }
}

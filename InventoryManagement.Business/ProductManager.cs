using InventoryManagement.Business.Contract;
using InventoryManagement.DataAccess;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Business
{
    public class ProductManager:IProductManager
    {
        ProductRepository objProductRepository = new ProductRepository();
        //public ResponseDetail AddCategoryDetails(CategoryDetails model)
        //{
        //    ResponseDetail objResponse = objProductRepository.AddCategoryDetails(model);
        //    return objResponse;
        //}
        //public ResponseDetail IsMasterExists(CheckDuplicateModel model)
        //{
        //    ResponseDetail objResponse = objProductRepository.IsMasterExists(model);
        //    return objResponse;
        //}
        //public ResponseDetail AddSubCategoryDetails(SubCategoryDetails model)
        //{
        //    ResponseDetail objResponse = objProductRepository.AddSubCategoryDetails(model);
        //    return objResponse;
        //}
        //public List<CategoryDetails> GetCategoryList(string ActiveFlag)
        //{
        //    List<CategoryDetails> objCategoryList = new List<CategoryDetails>();
        //    objCategoryList = objProductRepository.GetCategoryList(ActiveFlag);
        //    return objCategoryList;
        //}
        //public List<SubCategoryDetails> GetSubcategoryDetails(int CategoryId, string ActiveStatus)
        //{
        //    List<SubCategoryDetails> objSubCategoryList = new List<SubCategoryDetails>();
        //    objSubCategoryList = objProductRepository.GetSubcategoryDetails(CategoryId, ActiveStatus);
        //    return objSubCategoryList;
        //}
        //public ResponseDetail SaveProductMaster(ProductDetails model)
        //{
        //    ResponseDetail objResponse = new ResponseDetail();
        //    objResponse = objProductRepository.SaveProductMaster(model);
        //    return objResponse;
        //}
        //public int MaxBarCode()
        //{
        //    int MaxCode = objProductRepository.MaxBarCode();
        //    return MaxCode;
        //}
        //public int MaxProductCode()
        //{
        //    int MaxCode = objProductRepository.MaxProductCode();
        //    return MaxCode;
        //}
        public ResponseDetail AddCategoryDetails(CategoryDetails model)
        {
            ResponseDetail objResponse = objProductRepository.AddCategoryDetails(model);
            return objResponse;
        }
        public ResponseDetail IsMasterExists(CheckDuplicateModel model)
        {
            ResponseDetail objResponse = objProductRepository.IsMasterExists(model);
            return objResponse;
        }
        public ResponseDetail AddSubCategoryDetails(SubCategoryDetails model)
        {
            ResponseDetail objResponse = objProductRepository.AddSubCategoryDetails(model);
            return objResponse;
        }
        public List<CategoryDetails> GetCategoryList(string ActiveFlag)
        {
            List<CategoryDetails> objCategoryList = new List<CategoryDetails>();
            objCategoryList = objProductRepository.GetCategoryList(ActiveFlag);
            return objCategoryList;
        }
        public List<SubCategoryDetails> GetSubcategoryDetails(int CategoryId, string ActiveStatus)
        {
            List<SubCategoryDetails> objSubCategoryList = new List<SubCategoryDetails>();
            objSubCategoryList = objProductRepository.GetSubcategoryDetails(CategoryId, ActiveStatus);
            return objSubCategoryList;
        }
        public ResponseDetail SaveProductMaster(ProductDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse = objProductRepository.SaveProductMaster(model);
            return objResponse;
        }
        public int MaxBarCode()
        {
            int MaxCode = objProductRepository.MaxBarCode();
            return MaxCode;
        }
        public int MaxProductCode()
        {
            int MaxCode = objProductRepository.MaxProductCode();
            return MaxCode;
        }
        public List<ProductDetails> GetProductList(decimal LoginStateCode)
        {
            List<ProductDetails> objproductList = objProductRepository.GetProductList(LoginStateCode);
            return objproductList;
        }
        public ResponseDetail EditProductMaster(ProductDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse = objProductRepository.EditProductMaster(model);
            return objResponse;
        }
        public ResponseDetail DeleteProductMaster(ProductDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse = objProductRepository.DeleteProductMaster(model);
            return objResponse;
        }
        public ProductDetails GetProductDetail(decimal ProductId, decimal LoginStateCode)
        {
            ProductDetails objproduct = objProductRepository.GetProductDetail(ProductId,LoginStateCode);
            return objproduct;
        }
        public ResponseDetail SaveBarcode(List<BarcodeDetails> barcodeList)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse = objProductRepository.SaveBarcode(barcodeList);
            return objResponse;
        }
        
    }
}
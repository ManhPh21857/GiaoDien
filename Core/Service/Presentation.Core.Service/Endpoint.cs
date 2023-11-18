namespace Presentation.Core.Service;

public class Endpoint {
    public static string LOGIN => "api/v1/authentication/Login";
    public static string GET_USER_INFO => "api/v1/human-resources/User";
    public const string Register = "api/v1/Authentication/register";

    //color
    public const string GET_COLOR = "api/v1/common/Color";
    public const string UPDATE_COLOR = "api/v1/common/Color";
    public const string DELETE_COLOR = "api/v1/common/Color/delete";
    public const string REACTIVE_COLOR = "api/v1/common/Color/reactive";

    //Product
    public const string CREATE_PRODUCT = "api/v1/common/Product";
    public const string GET_PRODUCT_VIEW = "api/v1/common/Product/view/:pageNo";
    public const string GET_PRODUCT_ITEM = "api/v1/common/Product/item/:id";



    //Material
    public const string GET_MATERIAL = "api/v1/common/Material";
    public const string UPDATE_MATERIAL = "api/v1/common/Material";
    public const string DELETE_MATERIAL = "api/v1/common/Material/delete";
    public const string REACTIVE_MATERIAL = "api/v1/common/Material/reactive";
    //Origin
    public const string GET_ORIGIN = "api/v1/common/Origin";
    public const string UPDATE_ORIGIN = "api/v1/common/Origin";
    public const string DELETE_ORIGIN = "api/v1/common/Origin/delete";
    public const string REACTIVE_ORIGIN = "api/v1/common/Origin/reactive";
    //Supplier
    public const string GET_SUPLIER = "api/v1/common/Supplier";
    public const string UPDATE_SUPLIER = "api/v1/common/Supplier";
    public const string DELETE_SUPLIER = "api/v1/common/Supplier/delete";
    public const string REACTIVE_SUPLIER = "api/v1/common/Supplier/reactive";
    //Trademark
    public const string GET_TREDEMARK = "api/v1/common/Trademark";
    public const string UPDATE_TREDEMARK = "api/v1/common/Trademark";
    public const string DELETE_TREDEMARK = "api/v1/common/Trademark/delete";
    public const string REACTIVE_TREDEMARK = "api/v1/common/Trademark/reactive";
    //SIZE
    public const string GET_SIZE = "api/v1/common/Size";
    public const string UPDATE_SIZE = "api/v1/common/Size";
    public const string DELETE_SIZE = "api/v1/common/Size/delete";
    public const string REACTIVE_SIZE = "api/v1/common/Size/reactive";
    //Product
    public const string GET_PRODUCT = "api/v1/common/Product";
    public const string DELETE_PRODUCT = "api/v1/common/Product/:id";
    public const string GET_PRODUCT_DETAIL = "api/v1/common/Product/detail/:id";
    //CartDetail
    public const string GET_CARTDETAIL = "api/v1/sales/Cartdetail";
    public const string DELETE_CARTDETAIL = "api/v1/sales/Cartdetail/delete";
    public const string CREATE_CARTDETAIL = "api/v1/sales/Cartdetail";
    public const string UPDATE_CARTDETAIL = "api/v1/sales/Cartdetail";
    public const string GET_PRODUCTDETAILID = "api/v1/sales/Cartdetail/:productid/:colorid/:sizeid";
    
    //Classfication
    public const string GET_CLASSFICATION = "api/v1/common/Classification";
    public const string UPDATE_CLASSFICATION = "api/v1/common/Classification";
    public const string DELETE_CLASSFICATION = "api/v1/common/Classification/delete";
    public const string REACTIVE_CLASSFICATION = "api/v1/common/Classification/reactive";
}


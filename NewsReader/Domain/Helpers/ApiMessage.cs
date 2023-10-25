namespace NewsReader.Domain.Helpers
{
    public static class ApiMessage
    {
        public static string Successful = "عملیات با موفقیت انجام شد.";
        public static string Error = "عملیات با خطا انجام شد.";
        public static string NotFound = "اطلاعات مورد نظر یافت نشد.";

        //Website
        public static string WebsiteCreateSuccessful = "وب سایت مورد نظر با موفقیت ثبت گردید.";
        public static string WebsiteDeleteSuccessful = "وب سایت مورد نظر با موفقیت حذف گردید.";
        public static string WebsiteUpdateSuccessful = "وب سایت مورد نظر با موفقیت ویرایش گردید.";

        //WebsiteCategory
        public static string WebsiteCategoryDuplicateError = "دسته بندی مورد نظر تکراری می باشد.";
        public static string WebsiteCategoryCreateSuccessful = "دسته بندی وب سایت مورد نظر با موفقیت ثبت گردید.";
        public static string WebsiteCategoryDeleteSuccessful = "دسته بندی وب سایت مورد نظر با موفقیت حذف گردید.";
        public static string WebsiteCategoryUpdateSuccessful = "دسته بندی وب سایت مورد نظر با موفقیت ویرایش گردید.";

        //NewsCategory
        public static string NewsCategoryDuplicateError = "دسته بندی مورد نظر تکراری می باشد.";
        public static string NewsCategoryCreateSuccessful = "دسته بندی خبر مورد نظر با موفقیت ثبت گردید.";
        public static string NewsCategoryDeleteSuccessful = "دسته بندی خبر مورد نظر با موفقیت حذف گردید.";
        public static string NewsCategoryUpdateSuccessful = "دسته بندی خبر مورد نظر با موفقیت ویرایش گردید.";

        //Send Message
        public static string SendMessageSuccessful = "خبر با موفقیت ارسال گردید";
    }
}

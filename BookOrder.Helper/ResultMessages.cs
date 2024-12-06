namespace BookOrder.Helper
{
    /*
     * Developer Yorumu:
        Buradaki mesajlar key olarak düşünülüp Resources'dan sorgulanacak şekilde düzenlenebilir,
     */

    public static class ResultMessages
    {
        public const string BOOK_CANNOT_CREATE = "BOOK_CANNOT_CREATE";
        public const string BOOK_CANNOT_UPDATE = "BOOK_CANNOT_UPDATE";
        public const string BOOK_CANNOT_FIND = "BOOK_CANNOT_FIND";

        public const string CUSTOMER_CANNOT_CREATE = "CUSTOMER_CANNOT_CREATE";
        public const string CUSTOMER_EMAIL_ALREADY_USING_BY_ANOTHER_USER = "CUSTOMER_EMAIL_ALREADY_USING_BY_ANOTHER_USER";

        public const string LOGINUSER_CANNOT_FIND = "LOGINUSER_CANNOT_FIND";

        public const string ORDER_CANNOT_FIND_BOOK = "ORDER_CANNOT_FIND_BOOK";
        public const string ORDER_NOT_ENOUGHT_STOCK = "ORDER_NOT_ENOUGHT_STOCK";
        public const string ORDER_CANNOT_CREATE = "ORDER_CANNOT_CREATE";

        public const string STATISTIC_CANNOT_FIND_ANY_DATA = "STATISTIC_CANNOT_FIND_ANY_DATA";

    }
}

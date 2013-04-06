using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalHouseFinding.Common
{
    public static class ConstantColumnNameScoreNormalSearch
    {
        public const string DESCRIPTION_COLUMN_SCORE_NAME = "DescriptionColumnScore";
        public const string TITLE_COLUMN_SCORE_NAME = "TitleColumnScore";
        public const string STREET_COLUMN_SCORE_NAME = "StreetColumnScore";
        public const string NEARBY_COLUMN_SCORE_NAME = "NearbyColumnScore";
        public const string NUMBER_ADDRESS_COLUMN_SCORE_NAME = "NumberAddressColumnScore";
        public const string DIRECTION_COLUMN_SCORE_NAME = "DirectionColumnScore";
    }

    public static class ConstantCommonString
    {
        public const string NONE_INFORMATION = "NoneOfInformationText";
        public const string EXPIRED_DATE = "ExpiredDate";
        public const string EXPIRED_DATE_AFTER_RENEW = "ExpiredDateAfterRenew";

    }

    public static class ConstantMessageUserLog
    {
        public const string MESSAGE_CHANGE_STATUS_POST = "MessageChangeStatusPost";
        public const string MESSAGE_RECEIVE_QUESTION = "MessageReceiveQuestion";
        public const string MESSAGE_RECEIVE_ANSWER = "MessageReceiveAnswer";
    }

    public static class ConstantEmailTemplate
    {
        public const string RECEIVE_QUESTION = "YouReceiveQuestion";
        public const string RECEIVE_ANSWER = "YouReceiveMessage";
        public const string RECEIVE_FORGOT_PASSWORD = "ReceiveForgotPassword";
        public const string SUBJECT_RECEIVE_FORGOT_PASSWORD = "SubjectReceiveForgotPassword";
        public const string WELCOME = "Welcome";
        public const string SUBJECT_WELCOME = "SubjectWelcome";
        public const string WELCOME_OPENID = "WelcomeOpenId";
        public const string SUBJECT_WELCOME_OPENID = "SubjectWelcomeOpenId";
    }
}
namespace TravelBuddies.Application.Common.Exceptions.Messages
{
    public static class ExceptionMessages
    {
        public const string ApplicationUserNotFoundMessage = "Non-extitent User";
        public const string ApplicationUserNotCreatorMessage = "User is not creator";
        public const string ApplicationUserNotInGroupMessage = "User is not in group";
        public const string ApplicationUserAllreadyInGroupMessage = "User is allready in Group";
        public const string UserAllreadyIsBannedFromGroupMessage = "User is allready banned from group";
        public const string UnableToCreateApplicationUserMessage = "Unable to create user";
        public const string UnableToAddRoleToUserMessage = "Unable to add role to user";
        public const string InvalidLoginMessage = "Unable to login";

        public const string IdentityRoleNotFoundMessage = "Role not found";

        public const string PostNotFoundMessage = "Non-extitent Post";
        public const string NotAvailableSeatsInPostMessage = "No available seats in group";

        public const string GroupNotFoundMessage = "Non-extitent Group";
        public const string GroupNotMatchMessage = "Message group is not matching";

        public const string MessageNotFoundMessage = "Non-extitent Message";

        public const string CityNotFoundMessage = "Non-extitent City";

        public const string ReviewNotFoundMessage = "Non-extitent Review";

        public const string VehicleNotFoundMessage = "Vehicle not found";
    }
}

﻿namespace EducationPlatform.Constants
{
	public static class ValidationMessages
	{
		public const string RequiredFiled = "{0} is required.";
		public const string MinLength = "{0} must be at least {1} characters.";
		public const string MaxLength = "{0} cannot exceed {1} characters.";
		public const string EmailAddress = "Invalid Email Address.";
		public const string PhoneNumber = "Invalid Phone Number.";
		public const string MatchPassword = "New Password and Confirm New Password do not match.";
		public const string StringLent = "{0} must be between {1} and {2} characters.";
		public const string NotAllowedExtension = "Only .png, .jpg, .jpeg files are allowed!";
		public const string MaxSize = "File cannot be more that 2 MB!";
		public const string RangValue = "{0} must be between {1}% and {2}%.";
		public const string Rang = "{0} must be between {1} and {2}.";
		public const string stockQuntity = "The quantity for this product is currently zero, so its availability cannot be changed until stock is added. At the moment, this item is marked as unavailable. Please update the stock quantity to make it available again.";
		public const string ConfirmPasswordNotMatch = "The password and confirmation password do not match.";



		public const string MaxMinLength = "The {0} must be at least {2} and at max {1} characters long.";
		public const string Duplicated = "Another record with the same {0} is already exists!";
		public const string NotAllowFutureDates = "Date cannot be in the future!";
		public const string InvalidRange = "{0} should be between {1} and {2}!";
		public const string WeakPassword = "Passwords contain an uppercase character, lowercase character, a digit, and a non-alphanumeric character. Passwords must be at least 8 characters long";
		public const string InvalidUsername = "Username can only contain letters or digits.";
		public const string OnlyEnglishLetters = "Only English letters are allowed.";
		public const string OnlyArabicLetters = "Only Arabic letters are allowed.";
		public const string OnlyNumbersAndLetters = "Only Arabic/English letters or digits are allowed.";
		public const string DenySpecialCharacters = "Special characters are not allowed.";
		public const string InvalidMobileNumber = "Invalid mobile number.";
		public const string InvalidNationalId = "Invalid national ID.";
		public const string InvalidSerialNumber = "Invalid serial number.";
		public const string NotAvilableRental = "This book/copy is not available for rental.";
		public const string EmptyImage = "Please select an image.";
		public const string BlackListedSubscriber = "This subscriber is blacklisted.";
		public const string InactiveSubscriber = "This subscriber is inactive.";
		public const string MaxCopiesReached = "This subscriber has reached the max number for rentals.";
		public const string CopyIsInRental = "This copy is already rentaled.";
		public const string RentalNotAllowedForBlacklisted = "Rental cannot be extended for blacklisted subscribers.";
		public const string RentalNotAllowedForInactive = "Rental cannot be extended for this subscriber before renwal.";
		public const string ExtendNotAllowed = "Rental cannot be extended.";
		public const string PenaltyShouldBePaid = "Penalty should be paid.";
	}
}

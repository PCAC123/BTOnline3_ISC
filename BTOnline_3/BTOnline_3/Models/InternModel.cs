using System.ComponentModel.DataAnnotations;

namespace BTOnline_3.Models
{
    public class InternModel
    {
        public InternModel() { }
        public InternModel(int id, string? internName, string? internAddress, byte[]? imageData, DateTime? dateOfBirth, string? internMail, string? internMailReplace, string? university, string? citizenIdentification, DateTime? citizenIdentificationDate, string? major, bool? internable, bool? fullTime, string? cvfile, int? internSpecialized, string? telephoneNum, string? internStatus, DateTime? registeredDate, string? howToKnowAlta, string? internPassword, string? foreignLanguage, short? yearOfExperiences, bool? passwordStatus, bool? readyToWork, 
            bool? internEnabled, float? entranceTest, string? introduction, string? note, string? linkProduct, string? jobFields, bool? hiddenToEnterprise)
        {
            Id = id;
            InternName = internName;
            InternAddress = internAddress;
            ImageData = imageData;
            DateOfBirth = dateOfBirth;
            InternMail = internMail;
            InternMailReplace = internMailReplace;
            University = university;
            CitizenIdentification = citizenIdentification;
            CitizenIdentificationDate = citizenIdentificationDate;
            Major = major;
            Internable = internable;
            FullTime = fullTime;
            Cvfile = cvfile;
            InternSpecialized = internSpecialized;
            TelephoneNum = telephoneNum;
            InternStatus = internStatus;
            RegisteredDate = registeredDate;
            HowToKnowAlta = howToKnowAlta;
            InternPassword = internPassword;
            ForeignLanguage = foreignLanguage;
            YearOfExperiences = yearOfExperiences;
            PasswordStatus = passwordStatus;
            ReadyToWork = readyToWork;
            InternEnabled = internEnabled;
            EntranceTest = entranceTest;
            Introduction = introduction;
            Note = note;
            LinkProduct = linkProduct;
            JobFields = jobFields;
            HiddenToEnterprise = hiddenToEnterprise;
        }

        /// <summary>
        /// Unique identifier for the intern. (Primary Key)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Full name of the intern.
        /// </summary>
        public string? InternName { get; set; }

        /// <summary>
        /// Address of the intern.
        /// </summary>
        public string? InternAddress { get; set; }

        /// <summary>
        /// Image data (e.g., photo) of the intern.
        /// Stored as a byte array, typically for small images.
        /// </summary>
        public byte[]? ImageData { get; set; }

        /// <summary>
        /// Date of birth of the intern.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Primary email address of the intern.
        /// </summary>
        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [MinLength(5, ErrorMessage = "Email phải có ít nhất 5 ký tự.")]
        [MaxLength(255, ErrorMessage = "Email không được vượt quá 255 ký tự.")]
        public string? InternMail { get; set; }

        /// <summary>
        /// Alternate email address for the intern.
        /// </summary>
        public string? InternMailReplace { get; set; }

        /// <summary>
        /// University where the intern is studying/graduated.
        /// </summary>
        public string? University { get; set; }

        /// <summary>
        /// Citizen Identification Number of the intern.
        /// </summary>
        public string? CitizenIdentification { get; set; }

        /// <summary>
        /// Date of issue for the Citizen Identification.
        /// </summary>
        public DateTime? CitizenIdentificationDate { get; set; }

        /// <summary>
        /// Major field of study of the intern.
        /// </summary>
        public string? Major { get; set; }

        /// <summary>
        /// Indicates if the intern is currently internable.
        /// </summary>
        public bool? Internable { get; set; }

        /// <summary>
        /// Indicates if the intern is available for full-time.
        /// </summary>
        public bool? FullTime { get; set; }

        /// <summary>
        /// Path or reference to the intern's CV file.
        /// </summary>
        public string? Cvfile { get; set; }

        /// <summary>
        /// ID of the intern's specialization (e.g., Foreign Key to a Specialization table if one existed).
        /// </summary>
        public int? InternSpecialized { get; set; }

        /// <summary>
        /// Telephone number of the intern.
        /// </summary>
        public string? TelephoneNum { get; set; }

        /// <summary>
        /// Current status of the intern (e.g., "Active", "Completed", "Pending").
        /// </summary>
        public string? InternStatus { get; set; }

        /// <summary>
        /// Date the intern registered.
        /// </summary>
        public DateTime? RegisteredDate { get; set; }

        /// <summary>
        /// How the intern found out about Alta.
        /// </summary>
        public string? HowToKnowAlta { get; set; }

        /// <summary>
        /// Password for the intern's account (should be hashed in a real application).
        /// </summary>
        public string? InternPassword { get; set; }

        /// <summary>
        /// Foreign languages the intern knows.
        /// </summary>
        public string? ForeignLanguage { get; set; }

        /// <summary>
        /// Years of experience of the intern.
        /// </summary>
        public short? YearOfExperiences { get; set; }

        /// <summary>
        /// Indicates if the password needs to be changed.
        /// </summary>
        public bool? PasswordStatus { get; set; }

        /// <summary>
        /// Indicates if the intern is ready to start working.
        /// </summary>
        public bool? ReadyToWork { get; set; }

        /// <summary>
        /// Indicates if the intern's account is enabled.
        /// </summary>
        public bool? InternEnabled { get; set; }

        /// <summary>
        /// Score from the intern's entrance test.
        /// </summary>
        public float? EntranceTest { get; set; }

        /// <summary>
        /// Self-introduction by the intern.
        /// </summary>
        public string? Introduction { get; set; }

        /// <summary>
        /// Any additional notes about the intern.
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Link to the intern's product or portfolio.
        /// </summary>
        public string? LinkProduct { get; set; }

        /// <summary>
        /// Job fields the intern is interested in.
        /// </summary>
        public string? JobFields { get; set; }

        /// <summary>
        /// Indicates if the intern's profile is hidden from enterprises.
        /// </summary>
        public bool? HiddenToEnterprise { get; set; } = false;
    }
}


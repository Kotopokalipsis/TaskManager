using Core.Entities;

namespace Tests.Data.Fixtures
{
    public class EmployerFixture
    {
        public static Employer GetFirstEmployer()
        {
            var employer = new Employer();
            
            employer.FirstName = "Test First Name";
            employer.LastName = "Test Last Name";
            employer.PatronymicName = "Test Patronymic Name";
            employer.Email = "test@test.com";
            
            return employer;
        }
        
        public static Employer GetSecondEmployer()
        {
            var employer = new Employer();
            
            employer.FirstName = "Second First Name";
            employer.LastName = "Second Last Name";
            employer.PatronymicName = "Second Patronymic Name";
            employer.Email = "second@test.com";
            
            return employer;
        }
    }
}
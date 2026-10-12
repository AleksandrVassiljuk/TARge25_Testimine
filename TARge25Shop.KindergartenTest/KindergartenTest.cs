
using System;
using System.Threading.Tasks;
using TARge25Shop.Core.Dto;
using TARge25Shop.Core.ServiceInterface;
using Xunit;

namespace TARge25Shop.KindergartenTest
{
    public class KindergartenTest : TestBase
    {
        // 1. Kontrollime, et lasteaia loomine tagastab objekti.
        [Fact]
        public async Task ShouldNot_AddEmptyKindergarten_WhenResultIsReturned()
        {
            // Ülesseade
            KindergartenDto dto = MockKindergartenData();

            // Tegevus
            var result = await Svc<IKindergartenServices>().Create(dto);

            // Kontroll
            Assert.NotNull(result);
        }

        // 2. Kontrollime, et uuele lasteaiale luuakse ID.
        [Fact]
        public async Task Should_GenerateKindergartenId_WhenCreated()
        {
            // Ülesseade
            KindergartenDto dto = MockKindergartenData();

            // Tegevus
            var result = await Svc<IKindergartenServices>().Create(dto);

            // Kontroll
            Assert.NotEqual(Guid.Empty, result.Id);
        }

        // 3. Kontrollime, et grupi nimi salvestatakse õigesti.
        [Fact]
        public async Task Should_SaveGroupName_WhenKindergartenIsCreated()
        {
            // Ülesseade
            KindergartenDto dto = MockKindergartenData();

            // Tegevus
            var result = await Svc<IKindergartenServices>().Create(dto);

            // Kontroll
            Assert.Equal(dto.GroupName, result.GroupName);
        }

        // 4. Kontrollime, et laste arv salvestatakse õigesti.
        [Fact]
        public async Task Should_SaveChildrenCount_WhenKindergartenIsCreated()
        {
            // Ülesseade
            KindergartenDto dto = MockKindergartenData();

            // Tegevus
            var result = await Svc<IKindergartenServices>().Create(dto);

            // Kontroll
            Assert.Equal(dto.ChildrenCount, result.ChildrenCount);
        }

        // 5. Kontrollime, et lasteaia nimi salvestatakse õigesti.
        [Fact]
        public async Task Should_SaveKindergartenName_WhenKindergartenIsCreated()
        {
            // Ülesseade
            KindergartenDto dto = MockKindergartenData();

            // Tegevus
            var result = await Svc<IKindergartenServices>().Create(dto);

            // Kontroll
            Assert.Equal(dto.KindergartenName, result.KindergartenName);
        }

        // 6. Kontrollime, et õpetaja nimi salvestatakse õigesti.
        [Fact]
        public async Task Should_SaveTeacherName_WhenKindergartenIsCreated()
        {
            // Ülesseade
            KindergartenDto dto = MockKindergartenData();

            // Tegevus
            var result = await Svc<IKindergartenServices>().Create(dto);

            // Kontroll
            Assert.Equal(dto.TeacherName, result.TeacherName);
        }

        // 7. Kontrollime, et loomise kuupäev määratakse.
        [Fact]
        public async Task Should_SetCreatedAt_WhenKindergartenIsCreated()
        {
            // Ülesseade
            KindergartenDto dto = MockKindergartenData();

            // Tegevus
            var result = await Svc<IKindergartenServices>().Create(dto);

            // Kontroll
            Assert.NotEqual(DateTime.MinValue, result.CreatedAt);
        }

        // 8. Kontrollime, et olemasolev lasteaed leitakse ID järgi.
        [Fact]
        public async Task Should_GetKindergartenById_WhenIdExists()
        {
            // Ülesseade
            KindergartenDto dto = MockKindergartenData();
            var created = await Svc<IKindergartenServices>().Create(dto);

            // Tegevus
            var result = await Svc<IKindergartenServices>()
                .DetailAsync(created.Id.Value);

            // Kontroll
            Assert.NotNull(result);
            Assert.Equal(created.Id, result.Id);
        }

        // 9. Kontrollime, et vale ID puhul objekti ei leita.
        [Fact]
        public async Task ShouldNot_GetKindergartenById_WhenIdDoesNotExist()
        {
            // Ülesseade
            Guid wrongGuid = Guid.NewGuid();

            // Tegevus
            var result = await Svc<IKindergartenServices>()
                .DetailAsync(wrongGuid);

            // Kontroll
            Assert.Null(result);
        }

        // 10. Kontrollime, et grupi nime saab muuta.
        [Fact]
        public async Task Should_UpdateGroupName_WhenUpdatingData()
        {
            // Ülesseade
            KindergartenDto dto = MockKindergartenData();
            var created = await Svc<IKindergartenServices>().Create(dto);

            KindergartenDto updateDto = MockKindergartenData();
            updateDto.Id = created.Id;
            updateDto.CreatedAt = created.CreatedAt;
            updateDto.GroupName = "Liblikad";

            // Tegevus
            var result = await Svc<IKindergartenServices>().Update(updateDto);

            // Kontroll
            Assert.NotNull(result);
            Assert.Equal("Liblikad", result.GroupName);
            Assert.Equal(created.Id, result.Id);
        }

        // 11. Kontrollime, et laste arvu saab muuta.
        [Fact]
        public async Task Should_UpdateChildrenCount_WhenUpdatingData()
        {
            // Ülesseade
            KindergartenDto dto = MockKindergartenData();
            var created = await Svc<IKindergartenServices>().Create(dto);

            KindergartenDto updateDto = MockKindergartenData();
            updateDto.Id = created.Id;
            updateDto.CreatedAt = created.CreatedAt;
            updateDto.ChildrenCount = 25;

            // Tegevus
            var result = await Svc<IKindergartenServices>().Update(updateDto);

            // Kontroll
            Assert.NotNull(result);
            Assert.Equal(25, result.ChildrenCount);
        }

        // 12. Kontrollime, et õpetaja nime saab muuta.
        [Fact]
        public async Task Should_UpdateTeacherName_WhenUpdatingData()
        {
            // Ülesseade
            KindergartenDto dto = MockKindergartenData();
            var created = await Svc<IKindergartenServices>().Create(dto);

            KindergartenDto updateDto = MockKindergartenData();
            updateDto.Id = created.Id;
            updateDto.CreatedAt = created.CreatedAt;
            updateDto.TeacherName = "Anna";

            // Tegevus
            var result = await Svc<IKindergartenServices>().Update(updateDto);

            // Kontroll
            Assert.NotNull(result);
            Assert.Equal("Anna", result.TeacherName);
        }

        // 13. Kontrollime, et olematut lasteaeda ei saa muuta.
        [Fact]
        public async Task ShouldNot_UpdateKindergarten_WhenIdDoesNotExist()
        {
            // Ülesseade
            KindergartenDto dto = MockKindergartenData();
            dto.Id = Guid.NewGuid();

            // Tegevus
            var result = await Svc<IKindergartenServices>().Update(dto);

            // Kontroll
            Assert.Null(result);
        }

        // 14. Kontrollime, et lasteaed kustutatakse andmebaasist.
        [Fact]
        public async Task Should_RemoveKindergartenFromDatabase_WhenDeleted()
        {
            // Ülesseade
            KindergartenDto dto = MockKindergartenData();
            var created = await Svc<IKindergartenServices>().Create(dto);

            // Tegevus
            var deleted = await Svc<IKindergartenServices>()
                .Delete(created.Id.Value);

            var result = await Svc<IKindergartenServices>()
                .DetailAsync(created.Id.Value);

            // Kontroll
            Assert.NotNull(deleted);
            Assert.Equal(created.Id, deleted.Id);
            Assert.Null(result);
        }

        // 15. Kontrollime, et olematu ID kustutamine tagastab nulli.
        [Fact]
        public async Task ShouldNot_DeleteKindergarten_WhenIdDoesNotExist()
        {
            // Ülesseade
            Guid wrongGuid = Guid.NewGuid();

            // Tegevus
            var result = await Svc<IKindergartenServices>().Delete(wrongGuid);

            // Kontroll
            Assert.Null(result);
        }

        /* Üleval testid, all abimeetodid */

        private KindergartenDto MockKindergartenData()
        {
            return new KindergartenDto
            {
                GroupName = "Mesilased",
                ChildrenCount = 20,
                KindergartenName = "Tapa Lasteaed",
                TeacherName = "Mari",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
    }
}

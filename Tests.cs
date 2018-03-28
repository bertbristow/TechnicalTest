using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Interview
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Repository_Constructor_NullArgument_ThrowsArgumentNullException()
        {
            Type expectedResult = typeof(ArgumentNullException);
            ICollection<IStoreable> testArgument = null;

            TestDelegate codeUnderTest = () => new Repository<IStoreable>(testArgument);

            Assert.Throws(expectedResult, codeUnderTest);
        }

        [Test]
        public void Repository_Constructor_NoArgument_Succeeds()
        {
            IRepository<IStoreable> repoUnderTest = new Repository<IStoreable>();

            Assert.IsFalse(repoUnderTest.All().Any());
        }

        [Test]
        public void Repository_All_Returns_AllItems()
        {
            HashSet<IStoreable> testDataStore = new HashSet<IStoreable>
            {
                new Storeable(),
                new Storeable(),
                new Storeable(),
                new Storeable()
            };

            IRepository<IStoreable> repoUnderTest = new Repository<IStoreable>(testDataStore);

            IEnumerable<IComparable> testIdentifiers = testDataStore.Select(x => x.Id);
            IEnumerable<IComparable> result = repoUnderTest.All().Select(x => x.Id);

            Assert.IsFalse(testIdentifiers.Except(result).Any());
            Assert.IsFalse(result.Except(testIdentifiers).Any());
        }

        [Test]
        public void Repository_Save_AddsToStore()
        {
            IStoreable expectedResult = new Storeable();
            HashSet<IStoreable> testDataStore = new HashSet<IStoreable>();
            IRepository<IStoreable> testRepository = new Repository<IStoreable>(testDataStore);

            testRepository.Save(expectedResult);

            IStoreable result = testDataStore.FirstOrDefault();

            Assert.IsFalse(result == null);
            Assert.IsTrue(Equals(result.Id, expectedResult.Id));
            Assert.IsTrue(testDataStore.Count() == 1);
        }

        
        [Test]
        public void Repository_Save_WithNullArgument_ThrowsArgumentNullException()
        {
            Type expectedResult = typeof(ArgumentNullException);

            IRepository<IStoreable> testRepository = new Repository<IStoreable>();

            TestDelegate codeUnderTest = () => testRepository.Save(null);

            Assert.Throws(expectedResult, codeUnderTest);
        }


        [Test]
        public void Repository_Delete_WithExistingItem_RemovesItemFromStore()
        {
            int expectedResultCount = 0;

            IStoreable testStorable = new Storeable();
            HashSet<IStoreable> testDataStore = new HashSet<IStoreable>
            {
                testStorable
            };
            IRepository<IStoreable> testRepository = new Repository<IStoreable>(testDataStore);

            testRepository.Delete(testStorable.Id);

            int result = testDataStore.Count();

            Assert.IsTrue(result == expectedResultCount);
        }


        [Test]
        public void Repository_Delete_WithNonExistingItem_ThrowsInvalidOperationException()
        {
            Type expectedResult = typeof(InvalidOperationException);
            IStoreable testStorable = new Storeable();

            HashSet<IStoreable> testDataStore = new HashSet<IStoreable>
            {
                new Storeable()
            };
            IRepository<IStoreable> testRepository = new Repository<IStoreable>(testDataStore);

            TestDelegate codeUnderTest = () => testRepository.Delete(testStorable.Id);

            Assert.Throws(expectedResult, codeUnderTest);
        }

        [Test]
        public void Repository_Delete_WithNull_ThrowsArgumentNullException()
        {
            Type expectedResult = typeof(ArgumentNullException);

            IRepository<IStoreable> testRepository = new Repository<IStoreable>();

            TestDelegate codeUnderTest = () => testRepository.Delete(null);

            Assert.Throws(expectedResult, codeUnderTest);
        }


        [Test]
        public void Repository_FindById_WithNull_ThrowsArgumentNullException()
        {
            Type expectedResult = typeof(ArgumentNullException);

            IRepository<IStoreable> testRepository = new Repository<IStoreable>();

            TestDelegate codeUnderTest = () => testRepository.FindById(null);

            Assert.Throws(expectedResult, codeUnderTest);
        }

        [Test]
        public void Repository_FindById_WithValidId_ReturnsItem()
        {
            IStoreable expectedResult = new Storeable();
            HashSet<IStoreable> testDataStore = new HashSet<IStoreable>
            {
                expectedResult
            };
            IRepository<IStoreable> testRepository = new Repository<IStoreable>(testDataStore);

            IStoreable result = testRepository.FindById(expectedResult.Id);

            Assert.IsTrue(Equals(result.Id, expectedResult.Id));
        }

        [Test]
        public void Repository_FindById_WithInvalidId_ThrowsInvalidOperationException()
        {
            Type expectedResult = typeof(InvalidOperationException);

            HashSet<IStoreable> testDataStore = new HashSet<IStoreable>
            {
                new Storeable()
            };
            IRepository<IStoreable> testRepository = new Repository<IStoreable>(testDataStore);

            TestDelegate codeUnderTest = () => testRepository.FindById(Guid.NewGuid().ToString());

            Assert.Throws(expectedResult, codeUnderTest);
        }


        [Test]
        public void Storeable_Constructor_NullArgument_ThrowsArgumentNullException()
        {
            Type expectedResult = typeof(ArgumentNullException);

            TestDelegate codeUnderTest = () => new Storeable(null);

            Assert.Throws(expectedResult, codeUnderTest);
        }

        [Test]
        public void Storeable_Constructor_NoArgument_AutoGeneratesId()
        {
            Storeable expectedResult = new Storeable();
            
            Assert.IsTrue(expectedResult.Id.ToString().Length > 0);
        }
    }
}
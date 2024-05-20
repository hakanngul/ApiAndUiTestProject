**Project A: Interface Testing with C# SpecFlow**

This project aims to create an interface testing automation using C# SpecFlow. The project utilizes the SpecFlow test automation framework to write interface tests in the C# language.

**Technologies Used**
- C#
- SpecFlow
- Selenium

**Project Description**
The project utilizes SpecFlow along with Selenium to automate the testing of a web interface. SpecFlow allows for creating test scenarios using the Gherkin language, which are then mapped to C# source code. Selenium is used to automatically perform the steps of the test scenarios in a browser.

**Project B: API Test Project with NUnit C#**

This project aims to create an API testing automation using NUnit framework in C# language.

**Technologies Used**
- C#
- NUnit
- RestSharp

**Project Description**
The project utilizes NUnit and RestSharp to automate the testing of an API. Test scenarios are written in C# using NUnit, and RestSharp is used to create API requests and validate responses.

For detailed explanations and access to project files, please refer to the respective GitHub repositories.


# ApiAndUiTestProject

## Proje Hakkında

ApiAndUiTestProject, hem API hem de UI test otomasyonu için geliştirilmiş bir projedir. Proje, C# programlama dili ve SpecFlow, Selenium, NUnit, RestSharp gibi test otomasyon kütüphanelerini kullanır.

## Klasör Yapısı

- **PetStore**: Pet Store API testleri için oluşturulmuş proje.
- **SwagLabsProject**: Swag Labs UI testleri için oluşturulmuş proje.
- **pom.xml**: Maven yapılandırma dosyası.
- **.gitignore**: Git tarafından takip edilmeyecek dosyalar.

## Kullanılan Teknolojiler

- **C#**
- **SpecFlow**: UI test otomasyonu.
- **Selenium**: Web test otomasyonu.
- **NUnit**: API test otomasyonu.
- **RestSharp**: API istek ve cevaplarını yönetmek için.

## Kurulum ve Kullanım

1. Projeyi klonlayın:
   \`\`\`sh
   git clone https://github.com/hakanngul/ApiAndUiTestProject.git
   \`\`\`
2. Bağımlılıkları yükleyin:
   \`\`\`sh
   dotnet restore
   \`\`\`
3. Testleri çalıştırın:
   \`\`\`sh
   dotnet test
   \`\`\`

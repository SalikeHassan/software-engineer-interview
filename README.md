# Zip Software Engineer Interview

## Prerequisite to Run Code:
- [Download Visual Studio 2022 Community Version](https://code.visualstudio.com/download)<br/>
- [Download Postman](https://www.postman.com/downloads/)<br/>

## How to run code in Visual Studio 2022?
- Download or clone code from this repo.
- Open Visual Studio 2022.
- Browse solution file from cloned folder.
- Set Zip.Installments.Api as startup project as shown below screenshot.
  ![image](https://user-images.githubusercontent.com/18566830/213714130-9d2efc3e-0255-45ab-96c4-2b5f9f258355.png)

- Configure serilog in appsettings.json as below.
 ```
   "Serilog": {
    "using": [ "Serilog.Sinks.File" ],
    "Minimumlevel": {
      "Default": "Information"
    },
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "Path": "Logs\\ApplicationLog.log",
          "rollingInterval": "Day",
          "outputTemplate": "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}]"
        }
      }
    ]
  }
```
- After the above steps completed, run the project by clicking on Zip.Installments.Api button as shown in below screenshot.
  ![image](https://user-images.githubusercontent.com/18566830/213715132-d27ffe7e-f2d9-4f11-8a0c-3dd14c277ff1.png)

- On successfull run of the project, swagger ui will open in browser as shown in the below screenshot.
 ![image](https://user-images.githubusercontent.com/18566830/213716346-b2560090-0e64-47f0-af9f-15c0b72707e7.png)

## Assumption:
1. Minimum value for purchase amount will be $1.
2. Min value for number of installments is 1.
3. Min value for frequency is 1.

## Steps to create and get payment installment plan:
#### Create payment installment API details
1. Run the project and go to swagger ui in browser and click on the Post /api/v1/paymentinstallment section, and then click on Try it out button as shown in the below screenshot.
![image](https://user-images.githubusercontent.com/18566830/213718696-1a80a7fc-c25e-4736-b425-a9a330d2c6dc.png)

2. Provide the below request body and click on the Execute button as shown in below screenshot.
```
{
  "amount": 1000,
  "numofInstallment": 4,
  "frequency": 15
}
```
![image](https://user-images.githubusercontent.com/18566830/213720888-6107b4e4-ece2-4bba-a080-a9c9259f1fb7.png)

After the successfull Post request API will return the details of payment installment plan, as shown in the below screenshot. <br>
![image](https://user-images.githubusercontent.com/18566830/213721529-bc0c48ff-3ec5-4e83-98f9-c15ae8c8ce87.png)

#### Get payment installment API details
1. In swagger ui click on GET /api/v1/paymentinstallment/{id} section, and then click on Try it out button as shown in the below screenshot.
  ![image](https://user-images.githubusercontent.com/18566830/213723013-3085d57a-a2f3-43ed-b5b7-6d4ec357b165.png)
  
2. Provide the id of payment plan id, the below screenshot shows an example of id created when a payment installment plan created through POST request.
<img width="925" alt="image" src="https://user-images.githubusercontent.com/18566830/213723970-933a8412-f8dd-4cae-87e1-09c203c5aa2a.png">
Provide the id in textbox and then click on execute button, as shown in below screenshot.<br>
<img width="925" alt="image" src="https://user-images.githubusercontent.com/18566830/213724370-bd3b3996-4e14-4d07-b899-867ed7d06dbc.png">
For the valid id API will return the payment plan and installment plan as shown in the below screenshot.<br>
<img width="925" alt="image" src="https://user-images.githubusercontent.com/18566830/213724781-ae12289b-752a-4168-beff-f75c61c849ef.png">

## Steps to Run the Unit Test Cases:
- In Visual Studio 2022, Click View --> select Test Explorer, as shown in below screenshot.
  ![image](https://user-images.githubusercontent.com/18566830/213725986-a6d719b5-3a59-4f3e-8f64-22bd071c2626.png)

- System will open window as shown in below screenshot.
  ![image](https://user-images.githubusercontent.com/18566830/213655277-6b1b90be-860c-4bd1-a14a-6d5c03288c4c.png)
- Right click on Zip.Installments.ServiceTest and select run to run the unit test cases.

## Steps to Run the Integration Tests Using Postman:
- Open postman.
- Click on import in top left hand side. It opens below pop-up window.
 ![image](https://user-images.githubusercontent.com/18566830/213656020-6ff3f414-0b9c-4542-863e-0f3f9b9abbe3.png)
- Click on choose file and select file ZipCo.postman_collection from Zip.InstallmentsService\Zip.Installments.IntegrationTest.postman_collection location.
- System will show below screen. Click on import.
![image](https://user-images.githubusercontent.com/18566830/213656595-926716ae-933a-4bef-9c37-fa0c43d8051b.png)
- Once collection is imported --> Right click on collection and click on Run collection.
![image](https://user-images.githubusercontent.com/18566830/213658779-70ba7ba2-b481-4f43-8f47-d6d5ec39985b.png)
- Click on Run ZipCo button.
 ![image](https://user-images.githubusercontent.com/18566830/213659045-c8b05065-e6db-489e-b416-30c366948c51.png)
- System will run the integration test and show results as below.
 ![image](https://user-images.githubusercontent.com/18566830/213659390-c66e40f3-1130-4f39-969f-d1e0192ff85d.png)

## Project Solution Design:
- This system is designed using clean architecture. Below are the sub-component of this application.
- 1.Zip.Installments.Api:<br>
  This is project contains logic for payment installment plan.
- 2.Zip.Installments.Command:<br>
  This project contains logic to create payment and payment installment plan in database.
- 3.Zip.Installments.Common:<br>
  This project contains common functionalities which are used across projects.
- 4.Zip.Installments.Contract:<br>
  This project contains classes for API request body mapping and response.
- 5.Zip.Installments.Domain:<br>
  This project contains classes which defines the tables of payment and payment intallment plan.
- 6.Zip.Installments.Infrastructure:<br>
  This project contains classes to define database context object and domain classes configuration.
- 7.Zip.Installments.Query:<br>
  This project contains the logic to retrieve payment and payment installment plan data from database.
- 8.Zip.Installments.Service:<br>
  This project contains the logic of business requirement.
- 9.Zip.Installments.ServiceTest:<br>
  This project contains the unit test cases.
  
## Schema Design Details:
1. See below the schema details of request object **PaymentPlanRequest**.
 ```
 PaymentPlanRequest
{
  amount	number($double)
  numofInstallment integer($int32)
  frequency	integer($int32)
}
 ```
2. See below the schema details of response objects **PaymentPlanResponse**, and **InstallmentDetailsResponse**.
```
PaymentPlanResponse
{
  id	string($uuid)
  amount number($double)
  installments InstallmentDetailsResponse
}
```
```
InstallmentDetailsResponse
{
  dueDate	string
  nullable: true
  dueAmount	number($double)
  paymentId	string($uuid)
}
```
## Nugets Packages Used:
1. AutoFixture
2. FluentValidation.AspNetCore
3. FluentValidation.DependencyInjectionExtensions
4. Hellang.Middleware.ProblemDetails
5. MediatR
6. MediatR.Extensions.Microsoft.DependencyInjection
7. Microsoft.AspNetCore.Mvc.Versioning
8. Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
9. Microsoft.EntityFrameworkCore
10. Microsoft.EntityFrameworkCore.Design
11. Microsoft.EntityFrameworkCore.InMemory
12. Microsoft.EntityFrameworkCore.Tools
13. Microsoft.NET.Test.Sdk
14. Moq
15. NUnit
16. NUnit.Analyzers
17. NUnit3TestAdapter
18. Serilog.AspNetCore
29. Swashbuckle.AspNetCore

## Overview

Zip is a payment gateway that lets consumers split purchases into 4 interest free installments, every two weeks. The first 25% is taken when the purchase is made, and the remaining 3 installments of 25% are automatically taken every 14 days. We help customers manage their cash-flow while helping merchants increase conversion rates and average order values.

It may help to see our [product in action online](https://www.fanatics.com/mlb/new-york-yankees/new-york-yankees-nike-home-replica-custom-jersey-white/o-8976+t-36446587+p-2520909211+z-8-3193055640?_ref=p-CLP:m-GRID:i-r0c1:po-1), checkout our app on [ios](https://apps.apple.com/us/app/quadpay-buy-now-pay-later/id1425045070) or [android](https://play.google.com/store/apps/details?id=com.quadpay.quadpay&hl=en_US), and to read our documentation (https://docs.us.zip.co).

## Background

One of the cornerstones of Zip's culture is openness and transparency. When reviewing our existing interview structure, we found that pair-programming challenges rarely replicated what our employees actually do in their day-to-day work. For example, when was the last time you coded without google, or when the requirements weren't clearly defined? To tackle that, we've decided to publish our pair programming interview and share it directly with candidates beforehand.

As an Engineer at Zip you’ll help solve interesting problems on a daily basis. Some areas that you'll work on include fraud prevention, building real-time credit-decisioning models and, most importantly, shipping products that are secure, frictionless, and deliver a high-quality consumer experience.

The pair programming challenge will take an hour, and will more closely replicate a day-in-the-life at Zip. You’re free to use whichever resources help you to get the job done. When we evaluate your code at the end of the session, we will be looking for: 
- A high code health
- Simplicity
- Readability
- Presence of tests or planning for future tests
- And maintainability

While we mainly use .NET and C# in our back-end, we welcome candidates who are more familiar with other languages. We ask that you simply confirm your language with your recruiter beforehand. At the moment, we have only finalized starter code for C#, but feel free to look through that to prepare for your assignment even if using another language.

## The Pair Programming Interview

### The Challenge

During the interview, you will build a core service for our business, an Installment Calculator. There is no need to build anything before the interview, but feel free to investigate the boilerplate code and do some research on how you would set this up.

#### Installment Calculator
##### User Story

As a Zip Customer, I would like to establish a payment plan spread over 6 weeks that splits the original charge evenly over 4 installments.

##### Acceptance Criteria
- Given it is the 1st of January, 2020
- When I create an order of $100.00
- And I select 4 Installments
- And I select a frequency of 14 days
- Then I should be charged these 4 installments
  - `01/01/2020   -   $25.00`
  - `01/15/2020   -   $25.00`
  - `01/29/2020   -   $25.00`
  - `02/12/2020   -   $25.00`

## The System Design Interview

### Tools

For the system design interview, you are free to choose whatever tool you'd like. Our default is [Google Jamboard](https://edu.google.com/products/jamboard/?modal_active=none). If you have no preference on what tool you'd like to use, we recommend playing around with the jamboard a bit beforehand to learn the tips and tricks. Most interviewers find it's easier to use the stickynotes or textbox functions with drawn lines/arrows linking them together. If you prefer pen and paper or a whiteboard and marker, that's totally fine, just make sure your interviewer is able to see them every now and then if you're remote.

### Resources

There is absolutely no expectation for you to buy any books or online courses beforehand. Some of the following are links to resources that upsell to a paid course, but the free content should be good enough:

- [Grokking the System Design Interview - Build a URL Shortener](https://www.educative.io/courses/grokking-the-system-design-interview/m2ygV4E81AR)
- [System Design at Google](https://www.quora.com/What-is-the-system-design-interview-at-Google-like-for-a-SWE-position)

While we won't give you the exact prompt ahead of time, our general recommendation for all of our system design interviews is to:

- Ask questions
- Think about scalability
- Don't be afraid to completely change your design halfway through

## Closing Thoughts

We very much look forward to meeting you. Our goal is to make interviewers feel comfortable and prepared, so always feel free to reach out to your recruiter if you have any questions. Afterward, we welcome any and all feedback. We're constantly iterating and improving this process, and anything you share will help us make our interviews better for future candidates.

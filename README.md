# PlumsailTest
Backend (ASP.NET Core Web API)

1. Open in Visual Studio:
  - Open SubmissionWebApi.sln in Visual Studio.
2. Run the API:
  - Press F5 in Visual Studio or run

Frontend (Vue.js + Vite)

1. npm install
2. set API_URL in frontend\src\api\index.js to correct localhost address
3. npm run dev



PART 2

Data structure

Submission table with ID, name, formdata(json), createdDate
Files table with ID, SubmissionId, filename, storagePath, uploadDate
submissionId linking to a submission

async/await also can handle multiple file uploads at once but clients will still wait for the respond


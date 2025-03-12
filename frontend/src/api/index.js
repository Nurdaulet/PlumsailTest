import axios from 'axios';

const API_URL = 'http://localhost:5256/api/Submissions';

export async function submitFormData(data) {
  return axios.post(API_URL, data);
}

export async function fetchSubmissions(searchQuery = '', page = 1, pageSize = 5) {
  return axios.get(API_URL, {
        params: { search: searchQuery, page, pageSize }
    });
}

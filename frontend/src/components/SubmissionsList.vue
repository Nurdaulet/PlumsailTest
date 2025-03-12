<template>
    <div class="submissions-container">
        <h2>Submissions</h2>
        <input type="text" v-model="searchQuery" placeholder="Search..." @input="fetchSubmissions" />
        
        <!-- Show error if fetching fails -->
        <p v-if="errorMessage" class="error">{{ errorMessage }}</p>

        <table class="submissions-table">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Email</th>
                    <th>Category</th>
                    <th>Date</th>
                    <th>Newsletter</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="submission in submissions" :key="submission.id">
                    <td>{{ submission.name }}</td>
                    <td>{{ submission.email }}</td>
                    <td>{{ submission.category }}</td>
                    <td>{{ submission.date }}</td>
                    <td>{{ submission.newsletter ? "Yes" : "No" }}</td>
                </tr>
            </tbody>
        </table>

        <!-- Pagination Controls -->
        <div class="pagination">
            <button @click="changePage(1)" :disabled="page === 1">First</button>
            <button @click="changePage(page - 1)" :disabled="page === 1">Prev</button>
            
            <span>Page {{ page }} of {{ totalPages }}</span>

            <button @click="changePage(page + 1)" :disabled="page === totalPages">Next</button>
            <button @click="changePage(totalPages)" :disabled="page === totalPages">Last</button>
        </div>
    </div>
</template>


<script>
import { fetchSubmissions } from '../api';

export default {
    data() {
        return {
            submissions: [],
            searchQuery: '',
            errorMessage: '',
            page: 1,
            pageSize: 5, // Number of items per page
            totalPages: 1
        };
    },
    methods: {
        async fetchSubmissions() {
            this.errorMessage = ''; // Reset error before fetching
            try {
                const response = await fetchSubmissions(this.searchQuery, this.page, this.pageSize);
                this.submissions = response.data.data;
                this.totalPages = Math.ceil(response.data.total / this.pageSize);
            } catch (error) {
                this.errorMessage = 'Failed to fetch submissions. Please refresh the page.';
                console.error('Error fetching submissions:', error);
            }
        },
        changePage(newPage) {
            if (newPage >= 1 && newPage <= this.totalPages) {
                this.page = newPage;
                this.fetchSubmissions();
            }
        }
    },
    mounted() {
        this.fetchSubmissions();
    }
};
</script>

<style scoped>
@import '../assets/styles.css';
</style>

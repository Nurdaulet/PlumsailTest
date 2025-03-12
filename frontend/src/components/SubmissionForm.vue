<template>
  <div class="form-container">
    <h2>Submit a Form</h2>
    <form @submit.prevent="submitForm">
      <label>Name:</label>
      <input type="text" v-model="formData.name" />
      <span v-if="errors.name" class="error">{{ errors.name }}</span>

      <label>Email:</label>
      <input type="email" v-model="formData.email" />
      <span v-if="errors.email" class="error">{{ errors.email }}</span>

      <label>Category:</label>
      <select v-model="formData.category">
        <option value="general">General</option>
        <option value="support">Support</option>
        <option value="sales">Sales</option>
      </select>
      <span v-if="errors.category" class="error">{{ errors.category }}</span>

      <label>Date:</label>
      <input type="date" v-model="formData.date" />
      <span v-if="errors.date" class="error">{{ errors.date }}</span>

      <label>Newsletter:</label>
      <input type="checkbox" v-model="formData.newsletter" />

      <button type="submit">Submit</button>
    </form>
       <!-- Show error message if submission fails -->
       <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
  </div>
</template>

<script>
import { submitFormData } from '../api';

export default {
  data() {
    return {
      formData: {
        name: '',
        email: '',
        category: '',
        date: '',
        newsletter: false
      },
      errors: {},
       errorMessage: ''
    };
  },
  methods: {
    validateForm() {
      this.errors = {};
      if (!this.formData.name.trim()) this.errors.name = "Name is required.";
      if (!this.formData.email.trim()) {
        this.errors.email = "Email is required.";
      } else if (!/\S+@\S+\.\S+/.test(this.formData.email)) {
        this.errors.email = "Invalid email format.";
      }
      if (!this.formData.category) this.errors.category = "Category is required.";
      if (!this.formData.date) {
        this.errors.date = "Date is required.";
      } else if (new Date(this.formData.date) > new Date()) {
        this.errors.date = "Date cannot be in the future.";
      }
    },
    async submitForm() {
      this.validateForm();
      if (Object.keys(this.errors).length > 0) return;
      this.errorMessage = ''; // Reset error before submitting
      try {
        await submitFormData(this.formData);
        this.$emit('formSubmitted');
        this.formData = { name: '', email: '', category: '', date: '', newsletter: false };
      } catch (error) {
        this.errorMessage = 'Failed to submit the form. Please try again.';
        console.error('Error submitting form:', error);
      }
    }
  }
};
</script>

<style scoped>
@import '../assets/styles.css';
</style>

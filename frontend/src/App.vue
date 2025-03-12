<template>
  <div class="container">
    <SubmissionForm @formSubmitted="refreshSubmissions" />
    <SubmissionsList ref="submissionsList" />
  </div>
</template>

<script>
import SubmissionForm from './components/SubmissionForm.vue';
import SubmissionsList from './components/SubmissionsList.vue';

export default {
  components: { SubmissionForm, SubmissionsList },
  methods: {
    refreshSubmissions() {
      this.$refs.submissionsList.fetchSubmissions();
    }
  }
};
</script>

<style scoped>
@import './assets/styles.css';
</style>

<template>
  <AuthLayout>
    <v-row class="h-screen pb-16" justify="center">
      <v-col
        class="d-flex justify-center align-center"
        cols="12"
        lg="12"
        md="12"
        sm="12"
      >
        <h1>Зарегестрироваться как</h1>
      </v-col>
      <v-col
        v-for="(card, key) in authTypeCards"
        :key="key"
        cols="12"
        lg="2"
        md="4"
        sm="12"
      >
        <AuthSelectTypeCard :src="card.src" :title="card.title" :to="card.to" />
      </v-col>
    </v-row>
  </AuthLayout>
</template>

<script setup lang="ts">
  import { authTypeCards } from '@/components/features/auth/constants/'
  import AuthLayout from '@/layouts/AuthLayout.vue'
</script>

<style scoped lang="sass">

</style>

<template>
  <AuthLayout>
      <v-row justify="center" class="h-screen pb-16">
        <v-col cols="12" sm="12" md="12" lg="12" class="d-flex justify-center align-center">
          <h1>Войти как</h1>
        </v-col>
        <v-col cols="12" sm="12" md="4" lg="2" v-for="card in authTypeCards">
          <AuthSelectTypeCard :title="card.title" :src="card.src" :to="card.to" />
        </v-col>
      </v-row>
  </AuthLayout>
</template>

<script setup lang="ts">
import AuthLayout from '@/layouts/AuthLayout.vue';

const authTypeCards = [
  { title: 'Заказчик', src: '/src/assets/contact-book.svg', to: '/auth/login' },
  { title: 'Подрядчик', src: '/src/assets/document-sign.svg', to: '/auth/login' },
  { title: 'Испектор', src: '/src/assets/online-shop-laptop.svg', to: '/auth/login' },
] as const;
</script>

<style scoped lang="sass">

</style>

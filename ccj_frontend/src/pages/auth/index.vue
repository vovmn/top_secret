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
        <h1>Войти как</h1>
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
  import AuthLayout from '@/layouts/AuthLayout.vue'

  const authTypeCards = [
    { title: 'Заказчик', src: '/src/assets/contact-book.svg', to: '/auth/login' },
    { title: 'Подрядчик', src: '/src/assets/document-sign.svg', to: '/auth/login' },
    { title: 'Испектор', src: '/src/assets/online-shop-laptop.svg', to: '/auth/login' },
  ] as const
</script>

<style scoped lang="sass">

</style>

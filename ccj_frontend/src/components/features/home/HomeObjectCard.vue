<template>
  <v-card class="mx-auto" color="primary" max-width="400">
    <v-img class="align-end text-white" cover height="200" src="https://cdn.vuetifyjs.com/images/cards/docks.jpg">
      <v-card-title>{{ title }}</v-card-title>
    </v-img>

    <v-card-subtitle class="pt-4">
      Дата последней проверки {{ dateReview }}
    </v-card-subtitle>

    <v-card-text>
      <div>Объект</div>

      <div>Расположение {{ location }}</div>
    </v-card-text>

    <v-card-actions>
      <v-btn color="accent" text="Перейти" />
    </v-card-actions>
  </v-card>
</template>

<script setup lang="ts">
defineProps<{
    title: string
    location: string
    dateReview: string
  }>()
</script>

<style scoped lang="sass">

</style>

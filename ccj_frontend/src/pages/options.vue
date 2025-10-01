<template>
  <DefaultLayout>
    <BaseContainer>
      <h1 class="font-weight-bold mb-4">Настройки</h1>
      <v-switch v-model="isDarkTheme" color="success" hide-details label="Темная тема" />
      <v-switch v-model="getNotifications" color="success" hide-details label="Получать уведомления" />
    </BaseContainer>
  </DefaultLayout>
</template>

<script setup lang="ts">
  import { useTheme } from 'vuetify'
  import DefaultLayout from '@/layouts/DefaultLayout.vue'

  const theme = useTheme()

  const isDarkTheme = computed({
    get () {
      return theme.global.name.value === 'dark'
    },
    set (value: boolean) {
      const newTheme = value ? 'dark' : 'light'
      theme.global.name.value = newTheme
      localStorage.setItem('app-theme', newTheme)
    },
  })

  const getNotifications = ref(false)
</script>

<style scoped lang="sass">

</style>

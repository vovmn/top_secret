<template>
  <DefaultLayout>
    <v-container>
      <h1 class="font-weight-bold mb-4">Настройки</h1>
      <v-switch
        v-model="isDarkTheme"
        color="success"
        hide-details
        label="Темная тема"
        @click="theme.toggle()"
      />
    </v-container>
  </DefaultLayout>
</template>

<script setup lang="ts">
import DefaultLayout from '@/layouts/DefaultLayout.vue'
  import { useTheme } from 'vuetify'

  const theme = useTheme()

  const isDarkTheme = computed({
    get () {
      return theme.global.name.value === 'dark'
  },
    set (value: boolean) {
      const newTheme = value ? 'dark' : 'light'
      theme.global.name.value = newTheme
      localStorage.setItem('app-theme', newTheme)
    }
})

onMounted(() => {
    const savedTheme = localStorage.getItem('app-theme') as 'light' | 'dark' | null
    if (savedTheme) {
      theme.global.name.value = savedTheme
    }
})
</script>

<style scoped lang="sass">

</style>

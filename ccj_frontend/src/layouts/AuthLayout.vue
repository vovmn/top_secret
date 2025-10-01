<template>
  <v-main>
    <slot />
  </v-main>
</template>

<script lang="ts" setup>
  import { useTheme } from 'vuetify'

  const theme = useTheme()

  onMounted(() => {
    const savedTheme = localStorage.getItem('app-theme') as 'light' | 'dark' | null
    if (savedTheme) {
      theme.change(savedTheme)
    }
  })
</script>

<template>
  <v-app-bar color="primary">
    <v-app-bar-nav-icon variant="text" @click.stop="drawer = !drawer" />
  </v-app-bar>

  <v-navigation-drawer
    v-model="drawer"
    :color="primaryColorWhenDarkTheme"
    temporary
  >
    <v-list v-show="isAuthenticated">
      <v-list-item
        prepend-avatar="https://randomuser.me/api/portraits/women/90.jpg"
        subtitle="sandra_a88@gmailcom"
        title="Sandra Adams"
      />
    </v-list>

    <v-divider />

    <v-list v-if="isAuthenticated" density="compact" nav>
      <v-list-item
        :color="surfaceToAccentWhenDarkTheme"
        prepend-icon="mdi-account"
        title="Профиль"
        :to="'/profile'"
        value="profile"
      />
      <v-list-item
        :color="surfaceToAccentWhenDarkTheme"
        prepend-icon="mdi-domain"
        title="Объекты"
        :to="'/'"
        value="buildingObjects"
      />
      <v-list-item
        :color="surfaceToAccentWhenDarkTheme"
        prepend-icon="mdi-cog"
        title="Настройки"
        :to="'/options'"
        value="options"
      />
    </v-list>
    <v-container v-else class="px-4 pt-6">
      <v-btn
        block
        color="accent"
        elevation="2"
        to="/auth/login"
        variant="flat"
      >Вход</v-btn>
      <v-btn
        block
        class="mt-2"
        elevation="2"
        to="/auth/"
        variant="outlined"
      >Регистрация</v-btn>
    </v-container>
    <template #append>
      <div class="pa-4">
        <v-btn
          v-show="isAuthenticated"
          block
          elevation="2"
          :loading="isLoading"
          prepend-icon="mdi-logout"
          @click="clickToLogout"
        >
          Выйти
        </v-btn>
      </div>
    </template>
  </v-navigation-drawer>
</template>
<script setup>
  import { ref, watch } from 'vue'
  import { useTheme } from 'vuetify'
  import { useAuthStore } from '@/stores/auth'

  const drawer = ref(false)
  const group = ref(null)

  watch(group, () => {
    drawer.value = false
  })

  const authStore = useAuthStore()

  const isAuthenticated = computed(() => authStore.isAuthenticated)

  const theme = useTheme()

  const primaryColorWhenDarkTheme = computed(() => theme.current.value.dark ? 'background' : 'primary')
  const surfaceToAccentWhenDarkTheme = computed(() => theme.current.value.dark ? 'accent' : 'surface')

  const isLoading = ref(false)

  async function clickToLogout () {
    isLoading.value = true
    await nextTick()
    try {
      await new Promise(resolve => setTimeout(resolve, 500))
      authStore.logout()
    } finally {
      isLoading.value = false
    }
  }
</script>

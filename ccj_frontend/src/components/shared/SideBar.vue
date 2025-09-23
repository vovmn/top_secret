<template>
  <v-navigation-drawer expand-on-hover permanent rail :color="primaryColorWhenDarkTheme">
    <v-list>
      <v-list-item
        prepend-avatar="https://randomuser.me/api/portraits/women/90.jpg"
        subtitle="sandra_a88@gmailcom"
        title="Sandra Adams"
      />
    </v-list>

    <v-divider />

    <v-list density="compact" nav>
      <v-list-item
        :color="secondaryColorWhenDarkTheme"
        prepend-icon="mdi-domain"
        title="Объекты"
        :to="'/'"
        value="buildingObjects"
      />
      <v-list-item
        :color="secondaryColorWhenDarkTheme"
        prepend-icon="mdi-cog"
        title="Настройки"
        :to="'/options'"
        value="options"
      />
      <v-spacer />
      <v-list-item :color="secondaryColorWhenDarkTheme" prepend-icon="mdi-logout" title="Выйти" value="logout" />
    </v-list>
  </v-navigation-drawer>
</template>

<script setup lang="ts">
import { useTheme } from 'vuetify';

const theme = useTheme();
const secondaryColorWhenDarkTheme = computed(() => theme.global.name.value === 'dark' ? 'secondary' : 'surface')
const primaryColorWhenDarkTheme = computed(() => theme.global.name.value === 'dark' ? 'background' : 'primary')
</script>

<style scoped lang="sass">

</style>

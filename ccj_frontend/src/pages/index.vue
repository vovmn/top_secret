<template>
  <DefaultLayout>
    <BaseContainer>
      <h1 class="font-weight-bold mb-4">Объекты</h1>
      <v-card class="pt-4" color="background" elevation="0">
        <v-row align="center" align-content="center">
          <v-col cols="12" lg="3" md="4" sm="6">
            <HomeSearch v-model="search" />
          </v-col>

          <v-col cols="6">
            <v-tabs
              v-model="tab"
              align-tabs="center"
              :color="primaryToAccentWhenDarkTheme"
              :slider-color="primaryToAccentWhenDarkTheme"
              window
            >
              <v-tab class="rounded-sm mr-4" :value="1">
                Активирован
              </v-tab>
              <v-tab class="rounded-sm mr-4" :value="2">
                На контроле
              </v-tab>
              <v-tab class="rounded-sm " :value="3">
                Завершен
              </v-tab>
            </v-tabs>
          </v-col>
        </v-row>

        <v-tabs-window v-model="tab" class="mt-8">
          <v-tabs-window-item v-for="n in 3" :key="n" :value="n">
            <v-container fluid>
              <v-row>
                <v-col v-for="i in 6" :key="i" cols="12" md="4">
                  <HomeObjectCard
                    :date-review="`01.01.2025`"
                    :location="`${n}`"
                    :title="`Объект ${i}`"
                    :to="`/building-object/${i}`"
                  />
                </v-col>
              </v-row>
            </v-container>
          </v-tabs-window-item>
        </v-tabs-window>
      </v-card>
    </BaseContainer>
  </DefaultLayout>
</template>

<script lang="ts" setup>
  import { useTheme } from 'vuetify'

  import HomeObjectCard from '@/components/features/home/HomeObjectCard.vue'
  import DefaultLayout from '@/layouts/DefaultLayout.vue'

  const theme = useTheme()
  const primaryToAccentWhenDarkTheme = computed(() => theme.current.value.dark ? 'accent' : 'primary')

  const tab = ref(1)
  const search = ref('')
</script>

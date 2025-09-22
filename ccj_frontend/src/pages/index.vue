<template>
  <DefaultLayout>
    <v-container>
      <h1 class="font-weight-bold mb-4">Объекты</h1>
      <v-card elevation="2" class="pa-4">
        <v-row align="center" align-content="center" class="ma-4">
          <v-col cols="12" sm="6" md="4" lg="3">
            <HomeSearch v-model="search"></HomeSearch>
          </v-col>

          <v-col cols="6">
            <v-tabs v-model="tab" color="primary" dark slider-color="primary" align-tabs="center" window>
              <v-tab :value="1" class="mr-4">
                Активирован
              </v-tab>
              <v-tab :value="2" class="mr-4">
                На контроле
              </v-tab>
              <v-tab :value="3" class="">
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
                  <HomeObjectCard :title="`Объект ${i}`" :location="`${n}`" :date-review="`01.01.2025`">
                  </HomeObjectCard>
                </v-col>
              </v-row>
            </v-container>
          </v-tabs-window-item>
        </v-tabs-window>
      </v-card>
    </v-container>
  </DefaultLayout>
</template>

<script lang="ts" setup>
import HomeObjectCard from '@/components/features/home/HomeObjectCard.vue';
import DefaultLayout from '@/layouts/DefaultLayout.vue';

const tab = ref(1);
const search = ref('');
</script>

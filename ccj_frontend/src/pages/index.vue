<template>
  <DefaultLayout>
    <v-container grid-list-xs>
      <v-card>
        <v-tabs v-model="tab" color="primary" dark slider-color="primary" align-tabs="center" window>
          <v-tab :value="1">
            Активирован
          </v-tab>
          <v-tab :value="2">
            На контроле
          </v-tab>
          <v-tab :value="3">
            Завершен
          </v-tab>
        </v-tabs>

        <v-tabs-window v-model="tab">
          <v-tabs-window-item v-for="n in 3" :key="n" :value="n">
            <v-container fluid>
              <v-row>
                <v-col v-for="i in 6" :key="i" cols="12" md="4">
                  Объект {{ i }} вкладка {{ n }}
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
import DefaultLayout from '@/layouts/DefaultLayout.vue';

const tab = ref(1)
</script>

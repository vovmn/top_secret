<template>
  <DefaultLayout>
    <BaseContainer>
      <h1 class="font-weight-bold mb-8">Объект #{{ id }}</h1>
      <h2 class="mb-4 text-grey-darken-2">Сетевой график</h2>
      <v-row>
        <v-col cols="12" sm="10" md="8" lg="6">
          <v-table striped="even" density="compact" class="rounded-sm table-narrow border">
            <tbody>
              <tr v-for="(row, key) in tableHeadersAndKeys" :key="key">
                <td :style="`background-color: ${colors.secondary}`" class="text-white">
                  {{ row.header }}
                </td>
                <td>{{ tableData[row.key] }}</td>
              </tr>
            </tbody>
          </v-table>
        </v-col>
      </v-row>
    </BaseContainer>
  </DefaultLayout>
</template>

<script lang="ts" setup>
import DefaultLayout from '@/layouts/DefaultLayout.vue';
import { useTheme } from 'vuetify';

const theme = useTheme()
const colors = computed(() => theme.current.value.colors)

const route = useRoute('/building-object/[id]');
const id = route.params.id;

type TableDataKey = keyof typeof tableData;

const tableHeadersAndKeys = [
  { header: '№ Объекта', key: 'objectNumber' },
  { header: 'Задача', key: 'task' },
  { header: 'Полная задача', key: 'fullTask' },
  { header: 'Адрес', key: 'address' },
  { header: 'Дата начала', key: 'startDate' },
  { header: 'КПГЗ', key: 'kpgz' },
  { header: 'Объем', key: 'volume' },
  { header: 'Единицы измерения', key: 'unit' },
] satisfies { header: string, key: TableDataKey }[];

const tableData = {
  objectNumber: '001',
  task: 'Устройство фундамента',
  fullTask: 'Устройство монолитного ленточного фундамента с армированием',
  address: 'ул. Ленина, 15',
  startDate: '2025-04-01',
  kpgz: '01.23.45.67',
  volume: '45.5',
  unit: 'м³'
}
</script>

<style scoped lang="sass">
.table-narrow 
  /* Отключаем растягивание таблицы */
  width: fit-content;
  max-width: 100%;
  table-layout: auto;
</style>
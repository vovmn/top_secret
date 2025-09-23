<template>
  <DefaultLayout>
    <BaseContainer>
      <h1 class="font-weight-bold mb-8">Объект #{{ id }}</h1>
      <h2 class="mb-4 text-grey-darken-2">Сетевой график</h2>
      <v-row justify="space-between">
        <v-col cols="12" sm="12" md="7" lg="7">
          <v-table striped="even" density="compact" class="rounded-sm border">
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
        <v-col cols="12" sm="12" md="4" lg="4">
          <BuildingObjectContact class="mb-4" v-for="contact in contacts" :title="contact.title" :subtitle="contact.subtitle"
            :src="contact.src" />
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

const contacts = [
  {
    title: "Иван Иванов",
    subtitle: "Заказчик",
    src: "https://cdn.vuetifyjs.com/images/john.png"
  },
  {
    title: "Петр Петров",
    subtitle: "Инспектор",
    src: "https://cdn.vuetifyjs.com/images/john.png"
  },
  
] satisfies { title: string, subtitle: string, src: string }[];
</script>

<style scoped lang="sass">

</style>
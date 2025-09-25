<template>
  <DefaultLayout>
    <BaseContainer>
      <v-sheet class="mb-8 d-flex align-center ga-2" color="background">
        <h1 class="font-weight-bold">Объект #{{ id }}</h1>
        <v-chip outline :color="statuses[status].color">{{ statuses[status].name }}</v-chip>
      </v-sheet>

      <h2 class="mb-4 text-grey-lighten-1">Сетевой график</h2>
      <v-row justify="space-between">
        <v-col cols="12" sm="12" md="7" lg="7">
          <v-table striped="even" density="compact" class="rounded-sm border">
            <tbody>
              <tr v-for="(row, key) in Сonstants.headersAndKeysNetworkSheet" :key="key">
                <td :style="`background-color: ${colors.secondary}`" class="text-white">
                  {{ row.header }}
                </td>
                <td>{{ tableData[row.key] }}</td>
              </tr>
            </tbody>
          </v-table>
        </v-col>
        <v-col cols="12" sm="12" md="4" lg="4">
          <BuildingObjectContact class="mb-8" v-for="contact in contacts" :title="contact.title"
            :subtitle="contact.subtitle" :src="contact.src" />
        </v-col>

        <v-col cols="12" sm="12" md="7" lg="7">
          <h2 class="mb-4 text-grey-lighten-1">Товарно-транспортная накладная</h2>
          <v-data-table :group-by="Сonstants.tnnGroupBy" :headers="Сonstants.tnnHeaders" :items="tnnData" :items-per-page="-1 "
            item-value="product">
            <template v-slot:group-summary="{ item, columns }"></template>
          </v-data-table>
        </v-col>
      </v-row>
    </BaseContainer>
  </DefaultLayout>
</template>

<script lang="ts" setup>
import { useTheme } from 'vuetify';

import * as Сonstants from '@/components/features/building_object/constants/index';
import { statuses, type StatusKey } from '@/components/shared/constants/statuses';
import DefaultLayout from '@/layouts/DefaultLayout.vue';

const theme = useTheme();
const themeColors = theme.current.value.colors;
const colors = computed(() => themeColors);

const route = useRoute('/building-object/[id]');
const id = route.params.id;

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

const status: StatusKey = 'active';

const tnnData = [
  { product: 'Бетон М300', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: 'Ссылка' },
  { product: 'Бетон М301', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: 'Ссылка' },
  { product: 'Бетон М302', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: 'Ссылка' },
  { product: 'Бетон М303', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: 'Ссылка' },
  { product: 'Бетон М304', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: 'Ссылка' },
  { product: 'Бетон М305', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: 'Ссылка' },
  { product: 'Бетон М306', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: 'Ссылка' },
  { product: 'Бетон М307', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: 'Ссылка' },
  { product: 'Бетон М308', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: 'Ссылка' },
];
</script>

<style scoped lang="sass">

</style>
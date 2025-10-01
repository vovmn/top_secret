<template>
  <DefaultLayout>
    <BaseContainer>
      <h1 class="mb-8 font-weight-bold">График</h1>
      <h2 class="mb-4 text-grey-lighten-1">Сетевой график</h2>
      <v-row justify="space-between">
        <v-col cols="12" lg="7" md="7" sm="12">
          <v-table class="rounded-sm border" striped="even">
            <tbody>
              <tr v-for="(row, key) in headersAndKeysNetworkSheet" :key="key">
                <td class="text-white" :style="`background-color: ${colors.secondary}`">
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

<script setup lang="ts">
  import { useTheme } from 'vuetify'
  import { headersAndKeysNetworkSheet } from '@/components/features/building_object/constants'

  import DefaultLayout from '@/layouts/DefaultLayout.vue'

  const theme = useTheme()
  const colors = computed(() => theme.current.value.colors)

  const tableData = {
    objectNumber: '001',
    task: 'Устройство фундамента',
    fullTask: 'Устройство монолитного ленточного фундамента с армированием',
    address: 'ул. Ленина, 15',
    startDate: '2025-04-01',
    kpgz: '01.23.45.67',
    volume: '45.5',
    unit: 'м³',
  }
</script>

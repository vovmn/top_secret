<template>
	<DefaultLayout>
		<BaseContainer>
			<h1 class="mb-8 font-weight-bold">График</h1>
			<h2 class="mb-4 text-grey-lighten-1">Сетевой график</h2>
			<v-row justify="space-between">
				<v-col cols="12" sm="12" md="7" lg="7">
					<v-table striped="even" class="rounded-sm border">
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
			</v-row>
		</BaseContainer>
	</DefaultLayout>
</template>

<script setup lang="ts">
import * as Сonstants from '@/components/features/building_object/constants/index';
import DefaultLayout from '@/layouts/DefaultLayout.vue';
import { useTheme } from 'vuetify';

const theme = useTheme();
const themeColors = theme.current.value.colors;
const colors = computed(() => themeColors);

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

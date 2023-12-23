import { z } from 'zod';

export const transactionsImportSchema = z.object({
	importFile: z.any()
});

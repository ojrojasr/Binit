export class ProductDTO {
    id: string;
    createdAt: string;
    name: string;
    description: string;
    price: number;
    releaseDate: Date;
    categoryId: string;
    features: any[]; // must be FeatureDTO[]
    editors: any[]; // must be ProductEditorDTO[]
}